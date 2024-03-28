using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public Transform playerTransform;
    public Transform cameraTransform;

    void Start()
    {
        Debug.Log("Player movement script has loaded!");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        var movement = cameraTransform.forward * verticalMovement + Vector3.right * horizontalMovement;

        playerTransform.Translate(playerSpeed * Time.deltaTime * movement, Space.World);
    }
}