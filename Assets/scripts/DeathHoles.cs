using UnityEngine;

public class DeathHoles : MonoBehaviour
{
    [SerializeField] AudioSource bobSound;
    [SerializeField] AudioSource bobDieSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bobSound.enabled = false;
            bobDieSound.enabled = true;
            other.gameObject.SetActive(false);
            GameManager.Instance.Resetlevel(8f);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
