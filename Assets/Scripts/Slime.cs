using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Deactivate();
        }
    }
}
