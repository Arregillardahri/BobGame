using UnityEngine;

public class ShroomMan : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bobstate bobstate = collision.gameObject.GetComponent<Bobstate>();
            if (bobstate.starMan) 
            {
                Hit();
            }
            else if (collision.transform.HeadHit(transform, Vector2.down))
            {
                FlatShroom();
            }
            else
            {

                bobstate.Hit();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void FlatShroom()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<Runningbob>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 1f);
    }
   
    private void Hit()
    {
        GetComponent<Runningbob>().enabled = false;
        GetComponent<BobDeath>().enabled = true;
        Destroy(gameObject, 2f);
    }
}

