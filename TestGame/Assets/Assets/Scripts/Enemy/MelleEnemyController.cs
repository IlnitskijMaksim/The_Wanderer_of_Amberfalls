using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    [SerializeField] float chaseRadius = 10f; // Радиус преследования игрока
    [SerializeField] float walkRadius = 5f;   // Радиус случайного блуждания
    [SerializeField] float moveSpeed = 3f; // Параметр скорости
    [SerializeField] string wallTag = "Walls"; // Тег объектов, которые считаются стенами.

    private Transform player;                  // Ссылка на игрока
    private NavMeshAgent agent;                // Компонент NavMeshAgent
    private Vector3 randomDestination;         // Случайная точка назначения
    private bool isChasing = false;            // Флаг для отслеживания состояния преследования

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Устанавливаем параметр скорости
        agent.speed = moveSpeed;

        // Находим игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Запускаем случайное блуждание
        SetNewRandomDestination();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Если расстояние до игрока меньше радиуса преследования, преследуем игрока
            if (distanceToPlayer <= chaseRadius)
            {
                isChasing = true;
                agent.SetDestination(player.position);
            }
            else if (isChasing)
            {
                // Если преследование было активировано, но игрок вышел из радиуса, перейдем в случайное блуждание
                isChasing = false;
                SetNewRandomDestination();
            }
            else if (!isChasing && Vector3.Distance(transform.position, randomDestination) < 0.3f)
            {
                // Если не преследуем игрока и достигли случайной точки назначения, устанавливаем новую
                SetNewRandomDestination();
            }
        }
        else
        {
            isChasing = false;
        }
    }

    private void SetNewRandomDestination()
    {
        // Перед установкой новой точки назначения, проверяем, нет ли стены впереди.
        Vector3 newPosition = RandomNavMeshLocation();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, newPosition - transform.position, out hit, Vector3.Distance(transform.position, newPosition)) &&
            hit.collider.CompareTag(wallTag))
        {
            // Если есть стена, попробуйте снова через некоторое время.
            Invoke("SetNewRandomDestination", Random.Range(1f, 3f));
        }
        else
        {
            randomDestination = newPosition;
            agent.SetDestination(randomDestination);
        }
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);

        return hit.position;
    }
}
