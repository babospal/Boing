using Unity.VisualScripting;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int available = 3;
    public float bounceForce = 15f;

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
                // Finds the direction the top of the trampoline is pointing
                // (Works whether you rotated it 180 degrees OR set Scale Y to -1)
                Vector2 bounceDir = (transform.up * Mathf.Sign(transform.lossyScale.y)).normalized;

                // Check if the player hit the bouncy top face of the trampoline
                if (Vector2.Dot(collision.GetContact(0).normal, bounceDir) < -0.5f)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceDir.y * bounceForce);
                    if (audioSource != null && bounceSound != null)
                    {
                        audioSource.PlayOneShot(bounceSound);
                    }

                }
            }
        }
    }
}