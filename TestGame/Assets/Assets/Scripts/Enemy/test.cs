using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    [SerializeField] float chaseRadius = 10f; // Радиус преследования игрока
    [SerializeField] float walkRadius = 5f;   // Радиус случайного блуждания
    [SerializeField] string wallTag = "Walls"; // Тег объектов, которые считаются стенами.

    private Transform player;                  // Ссылка на игрока
    private NavMeshAgent agent;                // Компонент NavMeshAgent
    private Vector3 randomDestination;         // Случайная точка назначения
    private bool isChasing = false;            // Флаг для отслеживания состояния преследования

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Находим игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Запускаем случайное блуждание
        SetNewRandomDestination();
    }

    private void Update()
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
        else if (!isChasing && Vector3.Distance(transform.position, randomDestination) < 0.1f)
        {
            // Если не преследуем игрока и достигли случайной точки назначения, устанавливаем новую
            SetNewRandomDestination();
        }
    }

    private void SetNewRandomDestination()
    {
        // Генерируем случайную точку в радиусе блуждания
        randomDestination = RandomNavMeshLocation(walkRadius);

        // Проверяем, нет ли стены впереди
        RaycastHit hit;
        if (Physics.Raycast(transform.position, randomDestination - transform.position, out hit, Vector3.Distance(transform.position, randomDestination)) &&
            hit.collider.CompareTag(wallTag))
        {
            // Если есть стена, попробуйте снова через некоторое время.
            Invoke("SetNewRandomDestination", Random.Range(1f, 3f));
        }
        else
        {
            agent.SetDestination(randomDestination);
        }
    }

    private Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);

        return hit.position;
    }
}
