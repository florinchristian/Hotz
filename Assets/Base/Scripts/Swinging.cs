using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Swinging : MonoBehaviour
{
    [Header("Input")] 
    public KeyCode swingKey = KeyCode.Mouse0;
    public float swingSpeed;

    [Header("References")] 
    public LineRenderer lr;
    public Transform gunTip;
    public Transform cam;
    public Transform player;
    public LayerMask whatIsGrappleable;
    public Rigidbody rb;
    public Transform orientation;
    

    [Header("Swinging")] 
    private float maxSwingDistance = 100f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    private PlayerMovement pm;

    [Header("OdmGear")] public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendedCableSpeed;

    [Header("Prediction")] public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pm.swingSpeed = swingSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(swingKey)) StartSwing();
        if(Input.GetKeyUp(swingKey))StopSwing();
        
        CheckForSwingPoints();
        
        if(joint) OdmGearMovement();
    }

    private void CheckForSwingPoints()
    {
        if (joint) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward,
            out sphereCastHit, maxSwingDistance, whatIsGrappleable);
        
        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward,
            out raycastHit, maxSwingDistance, whatIsGrappleable);
        
        Vector3 realHitPoint;

        // Option 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;

        // Option 2 - Indirect (predicted) Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;

        // Option 3 - Miss
        else
            realHitPoint = Vector3.zero;

        // realHitPoint found
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        // realHitPoint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }
    private void LateUpdate()
    {
        DrawRope();
    }

    public void StartSwing()
    {
        
        // return if predictionHit not found
        if (predictionHit.point == Vector3.zero) return;
        
        // deactivate active grapple
        if(GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        pm.ResetRestrictions();
        
        pm.swinging = true;
        
            swingPoint = predictionHit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            
    }

    public void DrawRope()
    {
        if (!joint) return;
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0,gunTip.position);
        lr.SetPosition(1,swingPoint);
    }
    public void StopSwing()
    {
        pm.swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void OdmGearMovement()
    {
        //right
        if(Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right*horizontalThrustForce*Time.deltaTime);
        //left
        if(Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right*horizontalThrustForce*Time.deltaTime);
        //forward
        if(Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward*forwardThrustForce*Time.deltaTime);
        //shorten cable
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized*forwardThrustForce*Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
        //extend cable
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendedCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
        
    }
}
