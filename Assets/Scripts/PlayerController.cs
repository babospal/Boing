using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rigidbody;
    public float movementSpeed = 5;
    public float jumpForce = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rigidbody.linearVelocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, movementSpeed);
        }

    }

   
}
