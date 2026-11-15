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

            
                bool validHit = isNormalGravity
                    ? collision.contacts[0].normal.y < -0.5f
                    : collision.contacts[0].normal.y > 0.5f;

                if (validHit)
                {
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