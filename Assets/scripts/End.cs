using System.Collections;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] AudioSource bobSound;
    [SerializeField] AudioSource bobWinSound;

    public Transform flag;
    public Transform bottom;
    public Transform castle;

    public float speed = 6f;
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bobSound.enabled = false;
            bobWinSound.enabled = true;
            StartCoroutine(Movement(flag, bottom.position));
            StartCoroutine(EndLevel(other.transform));
        }
    }
    
    private IEnumerator EndLevel(Transform bobstate)
    {
        bobstate.GetComponent<Bob>().enabled = false;

        yield return Movement(bobstate, bottom.position);
        yield return Movement(bobstate, bobstate.position + Vector3.right);
        yield return Movement(bobstate, bobstate.position + Vector3.right + Vector3.down);
        yield return Movement(bobstate, castle.position);

        bobstate.gameObject.SetActive(false);
    }

    private IEnumerator Movement(Transform subject, Vector3 pos)
    {
        while (Vector3.Distance(subject.position, pos) > 0.13f)
        {
            subject.position = Vector3.MoveTowards(subject.position, pos, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = pos;
    }
}
