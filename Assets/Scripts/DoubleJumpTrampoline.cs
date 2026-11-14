using UnityEngine;

public class DoubleJumpTrampoline : MonoBehaviour
{
    public float bounceForce = 15f;
    public int grantedJumps = 1; // Gives 2 mid-air jumps (or set to 1 for a single extra air-jump)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (playerRb != null && player != null)
            {
                Vector2 bounceDirection = transform.up.normalized;

                if (Vector2.Dot(collision.GetContact(0).normal, bounceDirection) < -0.1f)
                {
                    // Launch the player in the direction the trampoline faces
                    playerRb.linearVelocity = bounceDirection * bounceForce;

                    // Grant double-jump charges
                    player.enabledJumpCounter = grantedJumps;
                }
            }
        }
    }
}