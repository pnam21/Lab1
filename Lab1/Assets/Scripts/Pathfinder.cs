using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner spawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    private void Awake()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
    }

    public void SetWaveConfig(WaveConfigSO value)
    {
        waveConfig = value;
    }

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            //Debug.Log($"Moving towards: {targetPosition} with speed {delta}");
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                //Debug.Log($"Reached waypoint {waypointIndex}");
                waypointIndex++;
            }
        }
        else
        {
            //Debug.Log("Destroying enemy.");
            Destroy(gameObject);
        }
    }
}
