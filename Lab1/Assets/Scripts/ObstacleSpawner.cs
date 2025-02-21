using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] float fallspeed = 5f;
    [SerializeField] float spawnCooldowns = 0.1f;
    [SerializeField] float spawnVariance = 0f;
    [SerializeField] float minimumSpawnCooldowns = 0.1f;

    Vector2 leftBound;
    Vector2 rightBound;
    Vector2 lowerBound;

    void Start()
    {
        InitBounds();
        StartCoroutine(SpawnObstacle());
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        leftBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 1));
        rightBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        lowerBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
    }

    Vector3 RandomSpawn()
    {
        return new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y + 1, 0);
    }

    IEnumerator SpawnObstacle()
    {
        do
        {
            for(int i =0; i < Random.Range(1, 4); i++)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, 1)], RandomSpawn(), Quaternion.Euler(0, 0, Random.Range(0, 359)), transform);
            }
            yield return new WaitForSeconds(GetSpawnCooldowns());
        }
        while (true);
    }
    public float GetFallSpeed()
    {
        return fallspeed;
    }

    public float GetSpawnCooldowns()
    {
        float spawnTime = Random.Range(spawnCooldowns - spawnVariance, spawnCooldowns + spawnVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnCooldowns, float.MaxValue);
    }
    
    public float GetLowerBound()
    {
        return lowerBound.y - 1;
    }
}
