using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody playerBody;
    
    private bool _isOnGround;
    
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        _isOnGround = false;
    }

    
    void FixedUpdate()
    {
        DetectJump();
    }
    
    void DetectJump()
    {
        if (Input.GetKey(KeyCode.Space) && _isOnGround)
        {
            playerBody.AddForce(0, 2f, 0, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            _isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            _isOnGround = false;
        }
    }
}
