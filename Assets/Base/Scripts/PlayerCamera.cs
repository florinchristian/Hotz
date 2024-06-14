using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSensitivity = 1.5f;
    
    public Transform cameraTransform;
    public Transform playerTransform;
    public Transform orientation;
    
    void Start()
    {
        Debug.Log("Player camera script has loaded!");       
    }

    
    void FixedUpdate()
    {
        if(CameraRotationController.instance.CanFollowMouse()) PerformViewMovement();
    }

    void PerformViewMovement()
    {
        var horizontalRotation = Input.GetAxis("Mouse X");
        var verticalRotation = Input.GetAxis("Mouse Y");
        
        playerTransform.Rotate(0, horizontalRotation * cameraSensitivity, 0);
        cameraTransform.Rotate(-verticalRotation * cameraSensitivity, 0, 0);
        
        
        
        orientation.rotation=Quaternion.Euler(0,horizontalRotation*cameraSensitivity,0);
    }
}
