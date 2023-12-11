using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speed = 2f;
    public float damage;
    public ToWeapon tw;
    private Rigidbody2D rb;

    void Start()
    {      
        {
            rb = GetComponent<Rigidbody2D>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 playerPosition = player.transform.position;
                Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
                Vector2 directionOther = (direction * 1000).normalized;
                rb.velocity = directionOther * speed;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если объект не является врагом
        if (!other.gameObject.CompareTag("Enemy") || !other.gameObject.CompareTag("Sword"))
        {
            PlayerStats enemy = other.GetComponent<PlayerStats>();
            if (enemy != null)
            {              
                enemy.GiveDamage(tw != null ? tw.getDamage() : damage);
            }
            Destroy(gameObject);
        }
        
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }
}
