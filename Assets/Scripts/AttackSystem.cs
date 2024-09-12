using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public int attackDamage;

    protected virtual void Start()
    {
        
    }

    public void Attack(HealthSystem target)
    {
        target.TakeDamage(attackDamage);
    }
}
