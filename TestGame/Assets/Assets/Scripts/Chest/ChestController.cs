using System.Collections;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] itemsToSpawn; // Массив предметов, которые могут выпасть из сундука
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            SpawnItems(); // Вызываем функцию создания предметов
            isOpen = true; // Помечаем сундук как открытый
            Destroy(gameObject); // Уничтожаем сундук, так как он больше не нужен
        }
    }

    private void SpawnItems()
    {
        // Создаем случайный предмет из массива и размещаем его перед сундуком
        if (itemsToSpawn.Length > 0)
        {
            int randomItemIndex = Random.Range(0, itemsToSpawn.Length);
            GameObject spawnedItem = Instantiate(itemsToSpawn[randomItemIndex], transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
