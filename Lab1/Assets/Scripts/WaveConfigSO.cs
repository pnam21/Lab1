using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float movespeed = 5f;
    [SerializeField] float spawnCooldowns = 1f;
    [SerializeField] float spawnVariance = 0f;
    [SerializeField] float minimumSpawnCooldowns = 0.2f;
    [SerializeField] float waveStartDelay = 0f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return movespeed;
    }

    public float GetSpawnCooldowns()
    {
        float spawnTime = Random.Range(spawnCooldowns - spawnVariance, spawnCooldowns + spawnVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnCooldowns, float.MaxValue);
    }

    public float GetWaveStartDelay()
    {
        return waveStartDelay;
    }
}
