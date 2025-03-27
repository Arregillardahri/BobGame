using UnityEngine;

public class Shellman : MonoBehaviour
{

    public float Shellspeed = 12f;

    public Sprite shellsprite;
    private bool shelled;
    private bool movingshell;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Bobstate bobstate = collision.gameObject.GetComponent<Bobstate>();

            if (bobstate.starMan)
            {
                Hit();
            }

            else if (collision.transform.HeadHit(transform, Vector2.down))
            {
                Shelly();
            }
            else
            {

                bobstate.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!movingshell)
            {
                Vector2 direction = new (transform.position.x - other.transform.position.x, 0f);
                Pushshell(direction);
            }
            else
            {
                Bobstate bobstate = other.GetComponent<Bobstate>();

                if (bobstate.starMan)
                {
                    Hit();
                }
                else
                {
                    bobstate.Hit();
                }

                
            }

        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void Shelly()
    {
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<Runningbob>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellsprite;
        
    }
    private void Pushshell(Vector2 direction)
    {
        movingshell = true;

        GetComponent<Rigidbody2D>().isKinematic = false;
        
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = Shellspeed;
        movement.enabled = true;


        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<Runningbob>().enabled = false;
        GetComponent<BobDeath>().enabled = true;
        Destroy(gameObject, 2f);
    }
}