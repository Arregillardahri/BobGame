using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class BobDeath : MonoBehaviour
{
    public Bob movement;
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Debug.Log("BobDeath OnEnable called!");
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
        
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if (deadSprite != null)
        {
            Debug.Log("BobDeath called!");
            spriteRenderer.sprite = deadSprite;
            

        }
    }
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;
        
        Runningbob runningbob = GetComponent<Runningbob>();
        EntityMovement entitymovement = GetComponent<EntityMovement>();

        if (entitymovement != null) 
        {
            entitymovement.enabled = false;
        }

        if (runningbob != null)
        {
            runningbob.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float duration = 3f;
        float elapsed = 0f;

        float jumpvelocity = 10f;
        float gravity = -30f;

        Vector3 velocity = Vector3.up * jumpvelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

