using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private DefaultSlimeHealth slimeHealth;
    private SlimeAttack slimeAttack;

    private void Start()
    {
        slimeHealth = GetComponent<DefaultSlimeHealth>();
        slimeAttack = GetComponent<SlimeAttack>();
    }

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
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(slimeAttack.attackDamage);
            }

            Deactivate();
        }
    }
}
