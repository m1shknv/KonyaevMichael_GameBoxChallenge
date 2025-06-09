using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] lootPrefabs;
    [SerializeField] private Transform spawnPoint;

    public void SpawnLoot()
    {
        if (lootPrefabs.Length == 0)
            return;

        int index = Random.Range(0, lootPrefabs.Length);
        GameObject loot = lootPrefabs[index];

        if (loot != null)
        {
            Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            Instantiate(loot, position, Quaternion.identity);
        }
    }
}
