using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Sound;

    public void PlaySound()
    {
        // Plays the clip once without interrupting other sounds on this source
        audioSource.PlayOneShot(Sound);
    }
}