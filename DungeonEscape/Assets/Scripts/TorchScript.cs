using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    public bool ActiveOnStart;
    public float TriggerRadius;
    public bool IsTrigger;

    public GameObject Light;
    public GameObject LightSphere;
    public Color color;
    public float LightRadius;
    public float LightIntensity;
    public bool flicker = true;
    private Light lightComp;

    private SphereCollider ICollider;

    private float noiseX = 0f;
    private float noiseY = 0f;
    private float baseIntensity = 0f;
    private void Awake()
    {
        lightComp = Light.GetComponent<Light>();
        lightComp.enabled = ActiveOnStart;
        lightComp.intensity = LightIntensity;
        lightComp.range = LightRadius;
        LightSphere.SetActive(ActiveOnStart);

        ICollider = gameObject.AddComponent<SphereCollider>();
        ICollider.isTrigger = IsTrigger;
        ICollider.radius = TriggerRadius;

        noiseY = Random.Range(0f,10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) 
        {
            lightComp.enabled = true;
            LightSphere.SetActive(true);
        }
    }

    public void SwitchOn() 
    {
        lightComp.enabled = true;
        LightSphere.SetActive(true);
    }

    public void SwitchOff()
    {
        lightComp.enabled = false;
        LightSphere.SetActive(false);
    }

    public void SetLightIntensity(float intensity) 
    {
        lightComp.intensity = intensity;
        this.LightIntensity = intensity;
    }

    public void SetLightRadius(float r) 
    {
        lightComp.range = r;
        this.LightRadius = r;
    }


    private void Update()
    {
        if (!flicker) return;
        if (noiseX >= 1) noiseX = 0f;
        noiseX += Time.deltaTime * Random.Range(1, 10);
        float noise = Mathf.PerlinNoise(noiseX, 0);

        lightComp.intensity = LightIntensity + (((noise - 0.5f) * 5) * Random.Range(0f,2f));
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,0,1);
        Gizmos.DrawWireSphere(gameObject.transform.position, TriggerRadius);
        Gizmos.color = new Color(1,1,0,1);
        Gizmos.DrawWireSphere(gameObject.transform.position, LightRadius);
    }
}
