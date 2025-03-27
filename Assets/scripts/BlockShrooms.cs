using System.Collections;
using UnityEngine;

public class BlockShrooms : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        circleCollider.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;


        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        Vector3 startpos = transform.localPosition;
        Vector3 endpos = transform.localPosition + Vector3.up;

        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startpos, endpos, t);
            elapsed += Time.deltaTime;
        }

        rigidbody.isKinematic = false;
        circleCollider.enabled = true;
        boxCollider.enabled = true; 
        
    }

}
