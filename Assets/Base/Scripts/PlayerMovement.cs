using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    
    public Transform playerTransform;
    public Transform cameraTransform;
    
    public float extraRunSpeed = 5.0f;
    
    private CabinetHide _isInThisCabinet;

    private const float SlideDuration = 1.0f;
    private const float SlideDistance = 50.0f;

    private float _currentSlideDuration = 0.0f;

    private bool _isCrouching;
    private bool _isSliding;

    private float _crouchDelay;
    private float _slideDelay;
    

    private bool _isRunning;

    private const float CrouchTimeout = 0.5f;
   

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _isCrouching = false;
        _isSliding = false;
        _isRunning = false;
        _crouchDelay = CrouchTimeout;
        _isInThisCabinet = null;

    }

    void FixedUpdate()
    {
        DetectRun();
        MovePlayer();
        DetectCrouch();
        DetectSlide();
        DetectHiding();
    }

    void DetectHiding()
    {  
        if (_isInThisCabinet != null)
        {
            _isInThisCabinet.Hide();
        }
    }

    public void SetCabinet(CabinetHide newCabinet)
    {
        _isInThisCabinet = newCabinet;
    }
    void DetectRun()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    void MovePlayer()
    {
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

        var totalSpeed = playerSpeed;
        
        if (_isRunning)
        {
            totalSpeed += extraRunSpeed;
        }
        
        playerTransform.Translate(totalSpeed * Time.deltaTime * movement, Space.World);
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
}