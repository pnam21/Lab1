using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    
    public void ShakeCamera()
    {
        //Debug.Log("Camera is shaking");
        if(cameraShake != null )
        {
            //Debug.Log("Camera is not null");
        } else
        {
            //Debug.Log("Camera is null");
        }
        //Debug.Log("Apply is " + applyCameraShake);
        //Debug.Log("Result are " + (cameraShake != null && applyCameraShake));
        if (cameraShake != null && applyCameraShake)
        {
            //Debug.Log("Camera is shaking immensely");
            cameraShake.Play();
        }
    }
}
