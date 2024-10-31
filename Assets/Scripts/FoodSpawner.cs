using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;


    public void SpawnFood()
    {
        Vector2 spawnPosition = GridManager.Instance.GetRandomGridPosition();
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}