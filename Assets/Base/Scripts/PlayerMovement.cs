using System;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    
    public Transform playerTransform;
    public Transform cameraTransform;

    public bool isWallRunning;
    public float extraRunSpeed = 5.0f;
    public float groundDrag;
    public bool activeGrapple;
    public bool swinging;
    public float swingSpeed;
    
    private Rigidbody playerBody;
    
    private const float SlideDuration = 1.0f;
    private const float SlideDistance = 50.0f;

    private float _currentSlideDuration = 0.0f;
    private float _totalSpeed;

    private bool _isCrouching;
    private bool _isSliding;
    public bool _freeze;
    private bool _enableMovementOnNextTouch;

    private float _crouchDelay;
    private float _slideDelay;
    
    public bool _isOnGround;
    private bool _isRunning;

    private const float CrouchTimeout = 0.5f;
    private Vector3 velocityToSet;

    void Start()
    {
       
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isCrouching = false;
            _isSliding = false;
            _isRunning = false;
            _crouchDelay = CrouchTimeout;
            playerBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (_freeze)
        {
            playerBody.velocity = Vector3.zero;
        }
        
        playerBody.drag =(_isOnGround && !activeGrapple)? groundDrag:0;
        SpeedControl();
    }

    void FixedUpdate()
    {
        if (CameraRotationController.instance.CanFollowMouse())
        {
            DetectRun();
            MovePlayer();
            DetectCrouch();
            DetectSlide();
        }
    }

    void DetectRun()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    void MovePlayer()
    {
        if (activeGrapple) return;
        
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        if (_isSliding)
        {
            if (_currentSlideDuration > SlideDuration)
            {
                _isSliding = false;
                _currentSlideDuration = 0;
                
                UncrouchPlayer();
            }

            _currentSlideDuration += Time.deltaTime;

            verticalMovement = (0.02f * SlideDistance) / SlideDuration;
        }

        var movement = cameraTransform.forward * verticalMovement + cameraTransform.right * horizontalMovement;

        _totalSpeed = playerSpeed;
        
        if (_isRunning || isWallRunning)
        {
            _totalSpeed += extraRunSpeed;
        }

        if (swinging)
            _totalSpeed = swingSpeed;
        
        
        if(_isOnGround || swinging) playerBody.AddForce(movement.normalized * (_totalSpeed * 10f), ForceMode.Force);
        //playerTransform.Translate(totalSpeed * Time.deltaTime * movement, Space.World);
    }

    void DetectCrouch()
    {
        _crouchDelay += Time.deltaTime;

        if (!Input.GetKey(KeyCode.C) || _crouchDelay < CrouchTimeout)
        {
            return;
        }
        
        if (_isCrouching)
        {
            UncrouchPlayer();
        }
        else
        {
            CrouchPlayer();
        }

        _isCrouching = !_isCrouching;

        _crouchDelay = 0.0f;
    }

    private void CrouchPlayer()
    {
        playerTransform.localScale += new Vector3(0, -0.5f, 0);
    }

    private void UncrouchPlayer()
    {
        playerTransform.localScale += new Vector3(0, 0.5f, 0);
    }

    private void DetectSlide()
    {
        if (Input.GetKey(KeyCode.E) && !_isSliding)
        {
            CrouchPlayer();

            _isSliding = true;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            _isOnGround = true;
        }

        if (_enableMovementOnNextTouch)
        {
            _enableMovementOnNextTouch = false;
            ResetRestrictions();
            GetComponent<Grappling>().StopGrapple();
        }
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            _isOnGround = false;
        }
    }

    private void SpeedControl()
    {

        if (activeGrapple) return;
        if (swinging) return;
        Vector3 flatVel = new Vector3(playerBody.velocity.x, 0f, playerBody.velocity.z);

        if (flatVel.magnitude > _totalSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _totalSpeed;
            playerBody.velocity = new Vector3(limitedVel.x, playerBody.velocity.y, limitedVel.z);
        }
    }
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) 
                                               + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
    private void SetVelocity()
    {
        _enableMovementOnNextTouch = true;
        playerBody.velocity = velocityToSet;

        
    }
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

}