using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Hurting")]
    [SerializeField] AudioClip hurtingClip;
    [SerializeField][Range(0f, 1f)] float hurtingVolume = 1f;

    [Header("Powerup")]
    [SerializeField] AudioClip powerupClip;
    [SerializeField][Range(0f, 1f)] float powerupVolume = 1f;

    [Header("Star")]
    [SerializeField] AudioClip starClip;
    [SerializeField][Range(0f, 1f)] float starVolume = 1f;

    [Header("Singleton")]
    [SerializeField] bool isSingleton;

    static AudioPlayer instance;

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            if (isSingleton)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            } else
            {
                Destroy(instance.gameObject);
                instance = null;
            }
        }
        else if (isSingleton)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }
    }

    public void PlayHurtingClip()
    {
        if (hurtingClip != null)
        {
            PlayClip(hurtingClip, hurtingVolume);
        }
    }

    public void PlayPowerUpClip()
    {
        if (powerupClip != null)
        {
            PlayClip(powerupClip, powerupVolume);
        }
    }

    public void PlayStarClip()
    {
        if (starClip != null)
        {
            PlayClip(starClip, starVolume);
        }
    }

    void PlayClip(AudioClip audio, float volume)
    {
        if (audio != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audio, cameraPos, volume);
        }
    }
}
