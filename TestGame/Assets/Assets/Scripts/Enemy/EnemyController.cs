using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRadius = 10f; // Радиус преследования игрока

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Если расстояние до игрока меньше радиуса преследования, двигаемся к игроку
        if (distanceToTarget <= chaseRadius)
        {
            agent.SetDestination(target.position);
        }
        else
        {

            agent.SetDestination(transform.position);
        }
    }
}