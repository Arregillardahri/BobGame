using System.Collections;
using UnityEngine;

public class Bobstate : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BobDeath bobDeath;

    public Bobrenderer smallRenderer;
    public Bobrenderer bigRenderer;
    private Bobrenderer activateRenderer;

    private CapsuleCollider2D capsuleCollider;

    [SerializeField] AudioSource BobHitSound;
    [SerializeField] AudioSource bobSound;
    [SerializeField] AudioSource bobDieSound;
    public bool SmallBob => smallRenderer.enabled;
    public bool BigBob => bigRenderer.enabled;
    public bool starMan {  get; private set; }
    public bool Dead => bobDeath.enabled;

  
    private void Awake()
    {
        activateRenderer = smallRenderer;
        bobDeath = GetComponent<BobDeath>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Hit()
    {

        Debug.Log($"Hit called. BigBob: {BigBob}, SmallBob: {SmallBob}");
        if (!Dead && !starMan)
        {
            if (BigBob)
            {
                Debug.Log("Player is big, shrinking");
                Shrink();
            }
            else
            {
                Debug.Log("Player is small, dying");
                Death();
            }
        }
    }

   

    public void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;   
        bobDeath.enabled = true;
        bobSound.enabled = false;
        bobDieSound.enabled = true;
        GameManager.Instance.Resetlevel(8f);
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;

        activateRenderer = bigRenderer;
        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(Animation());
    }

    private void Shrink()
    {
        BobHitSound.Play();
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;

        activateRenderer = smallRenderer;
        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) 
        {
            elapsed += Time.deltaTime;


            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activateRenderer.enabled = true;
    }

    public void Starman(float duration = 10f)
    {
        StartCoroutine(StarAnimate(duration));

    }

    private IEnumerator StarAnimate(float duration)
    {
        starMan = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activateRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activateRenderer.spriteRenderer.color = Color.white;
        starMan = false;
    }
}
