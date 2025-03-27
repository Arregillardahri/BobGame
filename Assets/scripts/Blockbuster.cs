using System.Collections;
using UnityEngine;

public class Blockbuster : MonoBehaviour
{
    

    public GameObject item;
    public int maxHits = -1;
    public Sprite emptyBlock;

    private bool animating;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.HeadHit(transform, Vector2.up))
            {
                
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if(maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null) 
        { 
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingpos = transform.localPosition;
        Vector3 animatedpos = restingpos + Vector3.up * 0.5f;

        yield return Move(restingpos, animatedpos);
        yield return Move(animatedpos, restingpos);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration) 
        { 
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }
}
