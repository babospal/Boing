using UnityEngine;

public class GravityTrampoline : MonoBehaviour
{
    public float bounceForce = 12f;
    [Header("Audio")]
    public AudioClip bounceSound;
    public AudioSource audioSource;
    public PlayerController playerControllerScript;
    private void Start()
    {
    playerControllerScript= GameObject.Find("Player").GetComponent<PlayerController>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerControllerScript.isInPlacementMode)
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float hitAlignment = Vector2.Dot(collision.contacts[0].normal, -transform.up);


                if (hitAlignment > 0.5f)
                {
                    bool isNormalGravity = playerRb.gravityScale > 0f;
                    playerRb.gravityScale *= -1f;

                    float launchDirection = isNormalGravity ? bounceForce : -bounceForce;
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, launchDirection);
                    if (audioSource != null && bounceSound != null)
                    {
                        audioSource.PlayOneShot(bounceSound);
                    }
                }
            }
        }
    }
}