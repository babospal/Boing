using UnityEngine;

public class DoubleJumpTrampoline : MonoBehaviour
{
    public float bounceForce = 15f;
    public int grantedJumps = 1;
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
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (playerRb != null && player != null)
            {
                Vector2 bounceDirection = transform.up.normalized;

                if (Vector2.Dot(collision.GetContact(0).normal, bounceDirection) < -0.1f)
                {
                    playerRb.linearVelocity = bounceDirection * bounceForce;

                    player.enabledJumpCounter = grantedJumps;
                    if (audioSource != null && bounceSound != null)
                    {
                        audioSource.PlayOneShot(bounceSound);
                    }
                }
            }
        }
    }
}