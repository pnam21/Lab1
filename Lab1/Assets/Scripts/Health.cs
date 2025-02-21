using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] bool isPowerUp;
    [SerializeField] bool isStar;
    SpecialEffect effect;
    [SerializeField] int health = 30;
    [SerializeField] int score = 30;
    [SerializeField] int powerUp;
    [SerializeField] bool isAdmin;
    
    private bool isInvincible = false;
    [SerializeField] private float invincibilityDuration = 2f;
    [SerializeField] private float flashInterval = 0.1f;
    private SpriteRenderer spriteRenderer;

    float powerUpDuration = 15f;
    float elapsedTime = 0;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Health powerupState;
    PickupSpawner pickupSpawner;

    private void Awake()
    {
        effect = GetComponent<SpecialEffect>();
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
        pickupSpawner = FindFirstObjectByType<PickupSpawner>();
        if(isPlayer)
        {
            spriteRenderer = transform.Find("playerShip1_blue_0").GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (powerUp != 0 && isPlayer && !isAdmin)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= powerUpDuration)
            {
                powerUp = 0;
                elapsedTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            if(isInvincible) return;
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayHurtingClip();
            effect.PlayHitEffect();
            effect.ShakeCamera();
            damageDealer.Hit();
        }
        else
        {
            powerupState = collision.GetComponent<Health>();
            if (powerupState != null && !isPowerUp)
            {
                powerUp = powerupState.GetPowerUp();
            }
            if (isPowerUp)
            {
                audioPlayer.PlayPowerUpClip();
            }
            if(isStar)
            {
                audioPlayer.PlayStarClip();
            }
            TakeDamage(0);
        }
    }

    public bool GetIsInvincible()
    {
        return isInvincible;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else if(isPlayer && damage != 0)
        {
            StartCoroutine(ActivateIFrames()); // Start invincibility frames
        }
    }

    public int GetPowerUp()
    {
        return powerUp;
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            if(!isPowerUp && powerUp != 0 && Random.Range(1, 10) == 1)
            {
                pickupSpawner.SpawnPowerUp(transform.position, powerUp);
            }
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    IEnumerator ActivateIFrames()
    {
        isInvincible = true;

        // Flash effect
        for (float t = 0; t < invincibilityDuration; t += flashInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(flashInterval);
        }

        spriteRenderer.enabled = true; // Ensure player is visible
        isInvincible = false; // End invincibility
    }

    public int GetHealth()
    {
        return health;
    }
}
