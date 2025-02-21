using UnityEngine;

public class PickupPath : MonoBehaviour
{
    PickupSpawner pickupSpawner;
    [SerializeField] float fallspeed = 5f;
    void Awake()
    {
        pickupSpawner = FindFirstObjectByType<PickupSpawner>();
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
        if (transform.position.y < pickupSpawner.GetLowerBound())
        {
            Destroy(gameObject);
        }
        float delta = fallspeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y - delta);
    }
}
