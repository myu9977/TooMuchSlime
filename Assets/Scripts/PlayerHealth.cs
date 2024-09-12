using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    public HealthBar healthBar;

    protected override void Start()
    {
        maxHealth = 100;
        base.Start();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(currentHealth);
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("플레이어 사망");
    }
}
