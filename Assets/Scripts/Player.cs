using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DefaultSlimeHealth slimeHealth = collision.collider.GetComponent<DefaultSlimeHealth>();
        if (slimeHealth != null)
        {
            playerAttack.Attack(slimeHealth);
        }
    }
}
