using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
    public float buletSpeed;
    private Rigidbody2D rb;
    public ToWeapon tw;
    private int destroy = 1;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*buletSpeed;
        Invoke("DestroyTime", destroy);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EntityStats enemy = other.GetComponent<EntityStats>();
        if (enemy != null)
        {
            enemy.GiveDamage(tw.getDamage());
        }
        Destroy(gameObject);
    }

}
