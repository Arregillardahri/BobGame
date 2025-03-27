using UnityEngine;
using UnityEngine.Rendering;

public class OneTouchSound : MonoBehaviour
{
    public AudioClip soundToPlay;
    private bool hasBeenPlayed = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has a Player tag and the sound hasn't been played yet
        if (other.CompareTag("Player") && !hasBeenPlayed)
        {
            // Try to get the AudioSource from the player
            AudioSource audioSource = other.GetComponent<AudioSource>();

            if (audioSource != null && soundToPlay != null)
            {
                
                // Play the sound
                audioSource.PlayOneShot(soundToPlay);

                // Mark as played to prevent repeating
                hasBeenPlayed = true;

                // Optional: Destroy the object after playing
                Destroy(gameObject);
            }
            
        }
    }
}