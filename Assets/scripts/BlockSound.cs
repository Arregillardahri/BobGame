using UnityEngine;

public class BlockSound : MonoBehaviour
{
    [SerializeField] AudioSource blockhit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        blockhit.volume = 10f;
            if (collision.transform.HeadHit(transform, Vector2.up))
            {
                blockhit.Play();
                
                
            }
        
    }




}