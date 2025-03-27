using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POWER : MonoBehaviour
{

    public enum Type
    {
        ExtraLife,
        MagicShroom,
        StarMan,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);


        }

    }

   

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicShroom:
                player.GetComponent<Bobstate>().Grow();

                break;
            case Type.StarMan:
                player.GetComponent<Bobstate>().Starman();


                break;
        }

        Destroy(gameObject);
    }
}

