using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")] 
    private Rigidbody rb;
    private PlayerMovement pm;
    public LayerMask whatIsWall;
    public Transform orientation;
    

    [Header("Climbing")] public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;
    private bool climbing;
    private bool isBlocked;
    private float horizontalMovement;

    [Header("Detection")] public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngleFront;
    private float wallLookAngleBack;
    private float wallLookAngleLeft;
    private float wallLookAngleRight;

    private RaycastHit frontWaallHit; 
    private RaycastHit backWallHit; 
    private RaycastHit rightWallHit;
    private RaycastHit leftWallHit;
    private bool wallFront;
    private bool wallBack;
    private bool wallRight;
    private bool wallLeft;
    private bool lateralWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        isBlocked = false;
    }

    private void Update()
    {
        WallCheck();
        StateMachine();
        if (climbing)
        {   
            StickOnTheWall();
            if(!isBlocked) ClimbingMovement();
            else MoveLateraly();
           
        }
    }

    private bool CanClimb()
    {
        if (wallFront && wallLookAngleFront < maxWallLookAngle)
            return true;
        if (wallBack && wallLookAngleBack < maxWallLookAngle)
            return true;
        if (wallRight && wallLookAngleRight < maxWallLookAngle)
            return true;
        if (wallLeft && wallLookAngleLeft < maxWallLookAngle)
            return true;
        return false;
    }

    
    private void StateMachine()
    {
        if (CanClimb() && (Input.GetKey(KeyCode.W)|| isBlocked))
        {
            if (!climbing && climbTimer > 0)
                    StartClimbing();
            

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if(climbTimer<0)StopClimbing();
        }
        else 
            if(climbing) StopClimbing();
    }
    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWaallHit,
            detectionLength,whatIsWall);
        wallBack = Physics.SphereCast(transform.position, sphereCastRadius, -orientation.forward, out backWallHit,
                detectionLength,whatIsWall);
        wallLeft=Physics.SphereCast(transform.position, sphereCastRadius, -orientation.right, out leftWallHit,
            detectionLength,whatIsWall);
        wallRight=Physics.SphereCast(transform.position, sphereCastRadius, orientation.right, out rightWallHit,
            detectionLength,whatIsWall);
        
        wallLookAngleFront = Vector3.Angle(orientation.forward, -frontWaallHit.normal);
        wallLookAngleBack=Vector3.Angle(-orientation.forward, -backWallHit.normal);
        wallLookAngleRight = Vector3.Angle(orientation.right, -rightWallHit.normal);
        wallLookAngleLeft = Vector3.Angle(-orientation.right, -leftWallHit.normal);

        if (pm._isOnGround)
        {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        
    }

    private void MoveLateraly()
    {
         float horizontalInput = Input.GetAxis("Horizontal");
         
         if (wallBack || wallRight)
             horizontalInput *= -1;
         
         if(wallFront || wallBack) rb.velocity = new Vector3(horizontalInput * climbSpeed, 0, 0);
         if (wallRight || wallLeft) rb.velocity = new Vector3(0, 0, horizontalInput * climbSpeed);
    }

    private void ClimbingMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (wallBack || wallRight)
            horizontalInput *= -1;
        
        if(wallFront || wallBack) rb.velocity = new Vector3(climbSpeed*horizontalInput, climbSpeed, rb.velocity.z);
        if (wallRight || wallLeft) rb.velocity = new Vector3(rb.velocity.x, climbSpeed, climbSpeed*horizontalInput);
    }

    private void StickOnTheWall()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isBlocked)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                isBlocked = true;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                isBlocked = false;
            }
        }
    }

    private void StopClimbing()
    {
        climbing = false;
        if (isBlocked)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            isBlocked = false;
        }

    }
}
