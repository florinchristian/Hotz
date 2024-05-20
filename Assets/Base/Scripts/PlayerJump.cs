using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody playerBody;
    
    private bool _isOnGround;
    
    void Start()
    {
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
            playerBody.AddForce(0, 1.5f, 0, ForceMode.Impulse);
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
