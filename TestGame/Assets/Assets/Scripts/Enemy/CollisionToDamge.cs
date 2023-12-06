using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private float entityDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string entityTag = collision.gameObject.tag;

        PlayerStats health = collision.gameObject.GetComponent<PlayerStats>();
        if (health != null)
        {
            health.GiveDamage(entityDamage);
        }
    }
}