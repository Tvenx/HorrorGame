using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 1.0f;
    public float speed = 2.0f;

    [SerializeField] private Transform playerTransform;
    public float nextSpawnTime;
    private Vector3 lastSpawnPosition;

    public List<GameObject> _target;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        lastSpawnPosition = spawnPoint.position;
    }

    public void SpawnPrefab()
    {
        Vector3 spawnPosition = lastSpawnPosition;
        Vector3 direction = (playerTransform.position - spawnPosition).normalized;

        spawnPosition += direction * spawnInterval;

        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        Rigidbody rb = newPrefab.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Обнуляем скорость по Y
            rb.velocity = new Vector3(direction.x * speed, 0f, direction.z * speed);
        }

        lastSpawnPosition = spawnPosition;
    }



}
