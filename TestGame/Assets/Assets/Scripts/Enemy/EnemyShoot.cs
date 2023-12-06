using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float speed;
    private float destroyTime = 3f;
    public float damage;
    public ToWeapon tw;

    void Start()
    {
        Invoke("DestroyTime", destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если объект не является текущим объектом (стрелой)
        if (!other.gameObject.CompareTag("Enemy"))
        {
            PlayerStats enemy = other.GetComponent<PlayerStats>();
            if (enemy != null)
            {
                // Check if the enemy object has an EntityStats component before accessing it
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
