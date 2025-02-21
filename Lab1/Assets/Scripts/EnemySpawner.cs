using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool isLooping;
    [SerializeField] List<WaveConfigSO> listWaveConfigSO;
    [SerializeField] float waveCooldowns = 0f;
    WaveConfigSO currentWaveConfigSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWaveConfig()
    {
        return currentWaveConfigSO;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            for (int waveIndex = 0; waveIndex < listWaveConfigSO.Count; waveIndex++)
            {
                currentWaveConfigSO = listWaveConfigSO[waveIndex];
                StartCoroutine(SpawnWave(listWaveConfigSO[waveIndex]));
            }

            // Wait before restarting all waves (if looping)
            yield return new WaitForSeconds(waveCooldowns);

        } while (isLooping);
    }

    IEnumerator SpawnWave(WaveConfigSO waveConfig)
    {
        yield return new WaitForSeconds(waveConfig.GetWaveStartDelay());

        for (int i = 0; i < waveConfig.GetEnemyCount(); i++)
        {
            GameObject enemy = Instantiate(
                waveConfig.GetEnemyPrefab(i),
                waveConfig.GetStartingWaypoint().position,
                Quaternion.Euler(0, 0, 180),
                transform
            );

            // Pass the waveConfig to the enemy's Pathfinder
            Pathfinder pathfinder = enemy.GetComponent<Pathfinder>();
            if (pathfinder != null)
            {
                pathfinder.SetWaveConfig(waveConfig);
            }

            yield return new WaitForSeconds(waveConfig.GetSpawnCooldowns());
        }
    }

}
