using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool gameOver = false;
    bool isOnGround = true;
    public Rigidbody2D rigidbody;
    public Collider2D playerCollider;
    public float movementSpeed = 5;
    public float jumpForce = 100;
    public float xScale;
    private Vector2 velocity = Vector2.zero;
    bool isOnZipline = false;
    SpriteRenderer ziplineSr;
    void Start()
    {
        xScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
      
        rigidbody.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rigidbody.linearVelocity.y);
        if(Input.GetAxis("Horizontal") <0 )
        {
            this.gameObject.transform.localScale = new Vector3(-xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        }
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, movementSpeed);
            isOnGround = false;
        }
        if(isOnZipline)
        {
            
            if (transform.position.x >= ziplineSr.bounds.max.x || transform.position.x <= ziplineSr.bounds.min.x)
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                isOnZipline = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log(collision);
        if (collision.CompareTag("DeadlyObject"))
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Zipline") && !isOnZipline) {
            Debug.Log(collision);
            ziplineSr = collision.gameObject.GetComponent<SpriteRenderer>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            isOnZipline = true;

        }
    }

}
