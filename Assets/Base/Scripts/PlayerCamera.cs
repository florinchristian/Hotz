using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSensitivity = 1.0f;
    
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
        
        playerTransform.Rotate(0, horizontalRotation * cameraSensitivity, 0);
        cameraTransform.Rotate(-verticalRotation * cameraSensitivity, 0, 0);
    }
}
