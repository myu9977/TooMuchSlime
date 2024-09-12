using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    protected override void Start()
    {
        maxHealth = 100;
        base.Start();
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("플레이어 사망");
    }
}
