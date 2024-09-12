using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSlimeHealth : HealthSystem
{
    public HealthBar healthBar;

    protected override void Start()
    {
        maxHealth = 50;
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
        gameObject.SetActive(false);
    }
}
