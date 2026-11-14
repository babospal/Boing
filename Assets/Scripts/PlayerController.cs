using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool gameOver = false;
    public bool isOnGround = true;
    public Rigidbody2D rigidbody;
    public Collider2D playerCollider;
    public float movementSpeed = 5;
    public float jumpForce = 100;
    public float xScale;
    public GameObject defaultTrampoline;
    //public GameObject largeTrampoline;
    public GameObject lowGravTrampoline;
    //public GameObject doubleJumpTrampoline;

    public GameObject currentSpawnedTrampoline;
   public    int selection = 0;
    public SpriteRenderer trampolineSelectorRenderer;

    public List<GameObject> selectableTrampolines = new List<GameObject>();
    bool isNormalGravity = true;
    private Vector2 velocity = Vector2.zero;
    bool isOnZipline = false;
    SpriteRenderer ziplineSr;
    public bool isInPlacementMode;


    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        xScale = gameObject.transform.localScale.x;
        selectableTrampolines.Add(defaultTrampoline);
        selectableTrampolines.Add(lowGravTrampoline);

    }

    // Update is called once per frame
    void Update()
    {
   
        if (isInPlacementMode)
        {
            trampolineSelectorRenderer.gameObject.SetActive(true);
            if (Input.GetKeyUp(KeyCode.RightArrow)){

               selection++;
            }
            //else if (Input.GetKeyUp(KeyCode.RightArrow))
            //{
            //    selection--;

            //}
            if (selection != selectableTrampolines.Count)
            {
                trampolineSelectorRenderer.sprite = selectableTrampolines[selection].GetComponent<SpriteRenderer>().sprite;
                if (Input.GetKeyUp(KeyCode.B))
                {
                    //currentSpawnedTrampoline = Instantiate(selectableTrampolines[selection].gameObject);
                    Instantiate(selectableTrampolines[selection].gameObject);
                }

            }
            //if(currentSpawnedTrampoline is not null && Input.GetKey(KeyCode.R))
            //{
            //    currentSpawnedTrampoline.transform.Rotate(new Vector3(0, 0, 0.3f));
            //}
           
            else
            {
                selection = 0;
            }

        }
        else
        {
            trampolineSelectorRenderer.gameObject.SetActive(false);

        }

        if (rigidbody.gravityScale < 0f)
        {
            isNormalGravity = false;

        }
        else
        {
            isNormalGravity = true;
        }

        if (isNormalGravity)
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 0);

        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 180);


        }
        rigidbody.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rigidbody.linearVelocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (isNormalGravity)
            {
                this.gameObject.transform.localScale = new Vector3(-xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            }
        }
        else
        {
            if (isNormalGravity)
            {
                this.gameObject.transform.localScale = new Vector3(xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(-xScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            }
        }


        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            if (isNormalGravity)
            {

                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);

            }
            else
            {

                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, -jumpForce);

            }
            isOnGround = false;
        }
        

        if (Input.GetKeyUp(KeyCode.E) && !isInPlacementMode)
        {
            isInPlacementMode = true;
            
        }
        else if (Input.GetKeyUp(KeyCode.E) && isInPlacementMode)
        {
            isInPlacementMode = false;

        }
        if (isOnZipline)
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Zipline") && !isOnZipline)
        {
            Debug.Log(collision);
            ziplineSr = collision.gameObject.GetComponent<SpriteRenderer>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            isOnZipline = true;
            
        }
    }

   

}
