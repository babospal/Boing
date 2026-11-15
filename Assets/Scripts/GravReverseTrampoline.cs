using UnityEngine;

public class GravityTrampoline : MonoBehaviour
{
    public float bounceForce = 12f;
    [Header("Audio")]
    public AudioClip bounceSound;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                bool isNormalGravity = playerRb.gravityScale > 0f;

                // Normal gravity: player lands on top of the trampoline
                // Inverted gravity: player falls upward into a ceiling trampoline
                bool validHit = isNormalGravity
                    ? collision.contacts[0].normal.y < -0.5f
                    : collision.contacts[0].normal.y > 0.5f;

                if (validHit)
                {
                    // Flip gravity (PlayerController will automatically flip the icon)
                    playerRb.gravityScale *= -1f;

                    // Launch toward the opposite surface
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