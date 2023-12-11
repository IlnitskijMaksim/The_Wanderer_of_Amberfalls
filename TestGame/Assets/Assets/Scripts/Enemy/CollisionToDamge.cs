using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private float entityDamage;
    [SerializeField] private float damageInterval = 2f; // »нтервал между ударами
    private float lastDamageTime; // ¬рем€ последнего удара

    private void OnCollisionStay2D(Collision2D collision)
    {
        string entityTag = collision.gameObject.tag;

        // ѕровер€ем, прошло ли достаточно времени с момента последнего удара
        if (Time.time - lastDamageTime >= damageInterval)
        {
            PlayerStats health = collision.gameObject.GetComponent<PlayerStats>();
            if (health != null)
            {
                health.GiveDamage(entityDamage);

                // ќбновл€ем врем€ последнего удара
                lastDamageTime = Time.time;
            }
        }
    }
}
