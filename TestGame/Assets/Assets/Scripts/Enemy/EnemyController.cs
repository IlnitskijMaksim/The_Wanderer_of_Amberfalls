using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRadius = 10f; // Радиус преследования игрока
    [SerializeField] float wanderRadius = 5f; // Радиус блуждания
    [SerializeField] LayerMask wallLayer; // Слой для стен

    NavMeshAgent agent;
    Vector3 startPosition;
    bool isWandering = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startPosition = transform.position;

        // Запускаем корутину для блуждания
        StartCoroutine(Wander());
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Если расстояние до игрока меньше радиуса преследования, двигаемся к игроку
        if (distanceToTarget <= chaseRadius)
        {
            agent.SetDestination(target.position);
            isWandering = false;
        }
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            // Бесконечно блуждаем, пока не преследуем игрока
            if (!isWandering)
            {
                Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += startPosition;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
                Vector3 finalPosition = hit.position;
                agent.SetDestination(finalPosition);
                isWandering = true;
            }

            // Подождите некоторое время перед повторным блужданием (например, 2 секунды)
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Если враг столкнулся со стеной, меняем направление
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 newDirection = Vector3.Reflect(agent.velocity.normalized, collision.contacts[0].normal);
            agent.SetDestination(transform.position + newDirection * 2f); // Добавляем смещение, чтобы избежать зацикливания
        }
    }
}
