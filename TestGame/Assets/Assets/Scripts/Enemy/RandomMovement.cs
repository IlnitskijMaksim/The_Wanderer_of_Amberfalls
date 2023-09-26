using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RandomMovement2D : MonoBehaviour
{
    public Rigidbody2D rb;

    [Range(0, 100)] public float speed;
    [Range(1, 500)] public float walkRadius;
    public string wallTag = "walls"; // Тег объектов, которые считаются стенами.

    private Vector2 randomDestination;
    private bool isMoving = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomDestination = RandomNavMeshLocation();
        isMoving = true;
    }

    public void Update()
    {
        if (isMoving)
        {
            Vector2 moveDirection = (randomDestination - rb.position).normalized;
            rb.velocity = moveDirection * speed;

            if (Vector2.Distance(rb.position, randomDestination) < 0.1f)
            {
                isMoving = false;
                rb.velocity = Vector2.zero;
                Invoke("SetNewDestination", Random.Range(1f, 3f));
            }
        }
    }

    private void SetNewDestination()
    {
        randomDestination = RandomNavMeshLocation();
        isMoving = true;
    }

    private Vector2 RandomNavMeshLocation()
    {
        Vector2 randomPosition = Random.insideUnitCircle * walkRadius;
        randomPosition += (Vector2)transform.position;

        return randomPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(wallTag))
        {
            // Если столкнулись с объектом, имеющим тег "walls", то меняем точку назначения.
            randomDestination = RandomNavMeshLocation();
        }
    }
}
