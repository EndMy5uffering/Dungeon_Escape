using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public float ColliderSize = 1f;

    public delegate void Collected(GameObject selfe);
    public static event Collected OnCollect;

    public bool CanCollect = true;
    public bool Animate = false;

    public float AnimationSpeed = 1f;
    public float hoverDelta = 0.005f;

    private float time = 0f;

    private Transform origin;

    private void Awake()
    {
        transform.gameObject.AddComponent<SphereCollider>();
        transform.gameObject.GetComponent<SphereCollider>().radius = ColliderSize;
        transform.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        this.origin = this.transform;
    }

    private void Update()
    {
        if (Animate) 
        {
            if (time >= 1)
            {
                time = 0;
            }

            transform.Rotate(transform.up, AnimationSpeed);
            transform.position = origin.position + (transform.up * Mathf.Lerp(hoverDelta, -hoverDelta, (0.5f * Mathf.Sin(2 * Mathf.PI * time) + 0.5f)));
            time += AnimationSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CanCollect) OnCollect?.Invoke(this.transform.gameObject);
    }

    public bool IsCanCollect() 
    {
        return CanCollect;
    }

    public void SetCanCollect(bool canCollect) 
    {
        this.CanCollect = canCollect;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,0,1);
        Gizmos.DrawWireSphere(transform.position, ColliderSize);
    }

}
