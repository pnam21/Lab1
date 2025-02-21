using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefabs;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Health health;

    private void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            switch (health.GetPowerUp())
            {
                case (1):
                    for (int i = 0; i < 3; i++)
                    {
                        StraightShooting();
                        yield return new WaitForSeconds(0.1f);
                    }
                    break;
                case (2):
                    DoubleShooting();
                    break;
                case (3):
                    TripleConeShooting();
                    break;
                default:
                    StraightShooting();
                    break;
            }

            float fireCooldowns = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);

            fireCooldowns = Mathf.Clamp(fireCooldowns, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(fireCooldowns);
        }
    }

    void DoubleShooting()
    {
        Vector3 rightShot = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        Vector3 leftShot = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
        List<GameObject> instances = new List<GameObject> {
            Instantiate(projectilePrefabs, rightShot, Quaternion.identity),
            Instantiate(projectilePrefabs, leftShot, Quaternion.identity)
        };
        foreach (var instance in instances)
        {
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
        }
    }

    void TripleConeShooting()
    {
        List<GameObject> instances = new List<GameObject> {
            Instantiate(projectilePrefabs, transform.position, Quaternion.Euler(0,0,16)),
            Instantiate(projectilePrefabs, transform.position, Quaternion.identity),
            Instantiate(projectilePrefabs, transform.position, Quaternion.Euler(0,0,-16))
        };
        int current = 0;
        foreach (var instance in instances)
        {
            Vector3 rightSpeed = current == 0 ? transform.right * -3f : current == 1 ? transform.right * 0 : transform.right * 3f;
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = transform.up * projectileSpeed + rightSpeed;
            }
            Destroy(instance, projectileLifetime);
            current++;
        }
    }

    void StraightShooting()
    {
        GameObject instance = Instantiate(projectilePrefabs, transform.position, Quaternion.identity);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.up * projectileSpeed;
        }
        Destroy(instance, projectileLifetime);
    }
}
