using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    
    void Start()
    {
        Debug.Log("Player camera script has loaded!");       
    }

    
    void FixedUpdate()
    {
        PerformViewMovement();
    }

    void PerformViewMovement()
    {
        var horizontalRotation = Input.GetAxis("Mouse X");
        var verticalRotation = Input.GetAxis("Mouse Y");
        
        playerTransform.Rotate(0, horizontalRotation, 0);
        cameraTransform.Rotate(-verticalRotation, 0, 0);
    }
}
