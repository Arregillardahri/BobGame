using System.Collections;
using UnityEngine;

public class MoneyMan : MonoBehaviour
{
  
    private void Start()
    {
        GameManager.Instance.AddMoney();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
       

        Vector3 restingpos = transform.localPosition;
        Vector3 animatedpos = restingpos + Vector3.up * 2f;

        yield return Move(restingpos, animatedpos);
        yield return Move(animatedpos, restingpos);

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.66f;

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

