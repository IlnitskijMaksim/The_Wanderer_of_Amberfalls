using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string playerTag = "Player"; // Тег игрока
    public float detectionRadius = 5f; // Радиус обнаружения игрока
    public float moveSpeed = 2f; // Скорость движения slime

    private Transform player;
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Найти игрока по тегу при старте
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void Update()
    {
        if (player == null)
        {
            // Игрок не найден, выходим
            return;
        }

        // Проверяем расстояние между slime и игроком
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Если игрок находится в радиусе обнаружения
        if (distanceToPlayer <= detectionRadius)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }

        // Если игрок в радиусе обнаружения, двигаем slime к игроку
        if (isPlayerInRange)
        {
            // Вычисляем направление движения к игроку
            Vector2 direction = (player.position - transform.position).normalized;

            // Двигаем slime в направлении игрока
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
