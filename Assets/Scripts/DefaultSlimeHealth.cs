using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSlimeHealth : HealthSystem
{
    protected override void Start()
    {
        maxHealth = 50;
        base.Start();
    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}
