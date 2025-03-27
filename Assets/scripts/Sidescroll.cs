using UnityEngine;
public class Sidescroll : MonoBehaviour
{
    private Transform Playamario;

    private void Awake()
    {
        Playamario = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraposition = transform.position;
        cameraposition.x = Mathf.Max(cameraposition.x, Playamario.position.x);
        transform.position = cameraposition;
    }
}
