using Unity.VisualScripting;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int available = 3;
    public float bounceForce = 15f;
    public PlayerController playerControllerScript;
    [Header("Audio")]
    public AudioClip bounceSound;
    public AudioSource audioSource;
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
              
                Vector2 bounceDir = (transform.up * Mathf.Sign(transform.lossyScale.y)).normalized;

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