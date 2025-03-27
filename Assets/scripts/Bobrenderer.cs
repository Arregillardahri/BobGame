using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Bobrenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    private Bob movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Runningbob run;

    private void Awake()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Bob>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {
        if (spriteRenderer.sprite.name != "Mario_Small_Death")
        {

            run.enabled = movement.running;

            if (movement.jumping)
            {
                spriteRenderer.sprite = jump;
            }
            else if (movement.sliding)
            {
                spriteRenderer.sprite = slide;
            }

            else if (!movement.running)
            {
                spriteRenderer.sprite = idle;
            }

        }
        else 
            { 
            movement.enabled = false;
            run.enabled = false; 
            }
    }
}

