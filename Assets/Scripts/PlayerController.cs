using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    public SpawnLimitsManager spawnLimitsManagerScript;

    public List<GameObject> spawnedNormals;
    public List<GameObject> spawnedLarges;
    public List<GameObject> spawnedLGs;
    public List<GameObject> spawnedx2Jumps;


    [Header("Audio")]
    public AudioClip deathSound;
    public AudioClip ziplineSound;
    [Range(0f, 1f)] public float volume = 1f;
    private AudioSource audioSource;

    bool gameOver = false;
    public bool isOnGround = true;
    public bool usedPlacementMode = false;
    public Rigidbody2D rigidbody;
    public Collider2D playerCollider;
    public int enabledJumpCounter = 1;
    public float movementSpeed = 5;
    public float jumpForce = 100;
    public float xScale;
    public GameObject defaultTrampoline;
    public GameObject largeTrampoline;
    public GameObject lowGravTrampoline;
    public GameObject doubleJumpTrampoline;

    public GameObject currentSpawnedTrampoline;
    public int selection = 0;
    public Image trampolineSelectorRenderer;

    public List<GameObject> selectableTrampolines = new List<GameObject>();
    bool isNormalGravity = true;
    private Vector2 velocity = Vector2.zero;
    bool isOnZipline = false;
    SpriteRenderer ziplineSr;
    public bool isInPlacementMode;
    public Canvas Canvas;
    public EventSystem eventSystem;
    public TextMeshProUGUI remainingCounter;

    private Vector3 offset;
    private Camera mainCamera;
    public Vector3 spawnPoint;
    public SpriteRenderer spriteRenderer;
    public Sprite defa;
    public Sprite fly;
    public Sprite zipline;
    void Start()
    {
        PhysicsMaterial2D noFriction = new PhysicsMaterial2D("No Friction") { friction = 0f, bounciness = 0f };
        playerCollider.sharedMaterial = noFriction;

        spawnLimitsManagerScript = gameObject.GetComponent<SpawnLimitsManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Instantiate(Canvas);
        Instantiate(eventSystem);
        trampolineSelectorRenderer = GameObject.Find("trampolineChoiceRenderer2").GetComponent<Image>();
        remainingCounter = GameObject.Find("RemainingCounter").GetComponent<TextMeshProUGUI>();
        
        spawnedNormals = new List<GameObject>();
        spawnedLarges = new List<GameObject>();
        spawnedLGs = new List<GameObject>();
        spawnedx2Jumps = new List<GameObject>();
        spawnPoint = Camera.main.transform.position + new Vector3(1,1,1);

        mainCamera = Camera.main;
        xScale = gameObject.transform.localScale.x;
        selectableTrampolines.Add(defaultTrampoline);
        selectableTrampolines.Add(lowGravTrampoline);
        selectableTrampolines.Add(largeTrampoline);
        selectableTrampolines.Add(doubleJumpTrampoline);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isInPlacementMode)
        {
            
            trampolineSelectorRenderer.gameObject.SetActive(true);
            remainingCounter.gameObject.SetActive(true);
            if (Draggable.selectedTrampoline is not null && Input.GetKey(KeyCode.R))
            {
                Debug.Log("rotating");
                Draggable.selectedTrampoline.transform.Rotate(new Vector3(0, 0, 1f));
            }

            
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {

                selection++;
            }
            //else if (Input.GetKeyUp(KeyCode.RightArrow))
            //{
            //    selection--;

            //}
            if (selection != selectableTrampolines.Count)
            {
                trampolineSelectorRenderer.sprite = selectableTrampolines[selection].GetComponent<SpriteRenderer>().sprite;

                if (selectableTrampolines[selection].name.Contains("trampoline_grav_reverse") )
                {
                    remainingCounter.text = $"{spawnLimitsManagerScript.spawnableLGCount - spawnedLGs.Count}";
                }
                if (selectableTrampolines[selection].name.Contains("Trampoline") )
                {
                    remainingCounter.text = $"{spawnLimitsManagerScript.spawnableNormalTCount - spawnedNormals.Count}";

                }
                if (selectableTrampolines[selection].name.Contains("BigTrampoline") )
                {
                    remainingCounter.text = $"{spawnLimitsManagerScript.spawnableLargeCount - spawnedLarges.Count}";

                }
                if (selectableTrampolines[selection].name.Contains("DoubleTrampoline") )
                {
                    remainingCounter.text = $"{spawnLimitsManagerScript.spawnablex2JumpCount - spawnedx2Jumps.Count}";

                }
                if (Input.GetKeyUp(KeyCode.B))
                {

                    if (selectableTrampolines[selection].name.Contains("trampoline_grav_reverse") && spawnedLGs.Count < spawnLimitsManagerScript.spawnableLGCount)
                    {
                        currentSpawnedTrampoline = Instantiate(selectableTrampolines[selection],spawnPoint, Quaternion.Euler(0, 0, 0));
                        spawnedLGs.Add(currentSpawnedTrampoline);
                    }
                    if (selectableTrampolines[selection].name.Contains("Trampoline") && spawnedNormals.Count < spawnLimitsManagerScript.spawnableNormalTCount)
                    {
                        Debug.Log(spawnedNormals.Count);
                        currentSpawnedTrampoline = Instantiate(selectableTrampolines[selection], spawnPoint , Quaternion.Euler(0, 0, 0));
                        spawnedNormals.Add(currentSpawnedTrampoline);
                    }
                    if (selectableTrampolines[selection].name.Contains("BigTrampoline") && spawnedLarges.Count < spawnLimitsManagerScript.spawnableLargeCount)
                    {
                        currentSpawnedTrampoline = Instantiate(selectableTrampolines[selection], spawnPoint, Quaternion.Euler(0, 0, 0));
                        spawnedLarges.Add(currentSpawnedTrampoline);
                    }
                    if (selectableTrampolines[selection].name.Contains("DoubleTrampoline") && spawnedx2Jumps.Count < spawnLimitsManagerScript.spawnablex2JumpCount)
                    {
                        currentSpawnedTrampoline = Instantiate(selectableTrampolines[selection], spawnPoint, Quaternion.Euler(0, 0, 0));
                        spawnedx2Jumps.Add(currentSpawnedTrampoline);
                    }

                    


                }

            }
            //if (currentSpawnedTrampoline is not null && Input.GetKey(KeyCode.R))
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
            remainingCounter.gameObject.SetActive(false);

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
        if(!isInPlacementMode)
        {
            rigidbody.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rigidbody.linearVelocity.y);

        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
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


        if (Input.GetKey(KeyCode.Space) && enabledJumpCounter > 0)
        {
            if (isNormalGravity)
            {

                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);

            }
            else
            {

                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, -jumpForce);

            }
            enabledJumpCounter--;
            isOnGround = false;
            spriteRenderer.sprite = fly;
        }


        if (Input.GetKeyUp(KeyCode.E) && !isInPlacementMode && !usedPlacementMode)
        {
            isInPlacementMode = true;

        }
        else if (Input.GetKeyUp(KeyCode.E) && isInPlacementMode)
        {
            isInPlacementMode = false;
            usedPlacementMode = true;
        }
        if (isOnZipline)
        {
            if (transform.position.x >= ziplineSr.bounds.max.x || transform.position.x <= ziplineSr.bounds.min.x)
            {
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                isOnZipline = false;
                spriteRenderer.sprite = fly;
                audioSource.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadlyObject"))
        {
            // Play the sound at the player's position
            AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);

            // Hide or disable the player so it looks dead immediately
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            rigidbody.simulated = false; // Stop physics interactions

            // Start a coroutine to reload the scene after a short delay
            StartCoroutine(ReloadSceneAfterDelay(1f)); // 1 second delay (adjust as needed based on sound length)
        }
    }

    private System.Collections.IEnumerator ReloadSceneAfterDelay(float delay)
    {
        // Wait for the specified amount of seconds (realtime ensures it works even if timeScale is 0)
        yield return new WaitForSecondsRealtime(delay);

        // Now reload the scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;

                bool hitValidSurface = isNormalGravity
                    ? normal.y > 0.5f
                    : normal.y < -0.5f;

                if (hitValidSurface)
                {
                    isOnGround = true;
                    enabledJumpCounter = 1; // Reset back to standard 1 jump on landing
                    spriteRenderer.sprite = defa;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Zipline") && !isOnZipline)
        {
            Debug.Log(collision);
            ziplineSr = collision.gameObject.GetComponent<SpriteRenderer>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            isOnZipline = true;
            spriteRenderer.sprite = zipline;
            if (ziplineSound != null)
            {
                audioSource.clip = ziplineSound;
                audioSource.volume = volume;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}
