using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
    }

    //private void Start()
    //{
    //    healthBar.maxValue = 100;
    //    healthBar.value = 100;
    //}

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
}
