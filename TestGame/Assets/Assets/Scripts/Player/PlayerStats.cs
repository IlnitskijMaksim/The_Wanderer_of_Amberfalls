using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float health;
    public float maxHealth;

    public HealthBar healthBar;

    public void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void GiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.SetHealth((int)health);
    }

    public void GiveHealth(float addHealth)
    {
        health += addHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

}