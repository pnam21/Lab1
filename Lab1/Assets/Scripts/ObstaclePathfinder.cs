using UnityEngine;

public class ObstaclePath : MonoBehaviour
{
    ObstacleSpawner obstacleSpawner;
    [SerializeField] float fallspeed = 5f;
    void Awake()
    {
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Fall();
    }

    void Fall()
    {
        if(transform.position.y < obstacleSpawner.GetLowerBound())
        {
            Destroy(gameObject);
        }
        float delta = fallspeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y - delta);
    }
}
