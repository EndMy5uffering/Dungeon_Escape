using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0, 0, 0);

    [Range(0f, 1f)]
    public float strength = 0.05f;

    [Range(0f, 1f)]
    public float drag = 0.3f;

    public GameObject Target;

    public bool AjustParentRotation = true;
    public bool FollowTarget = true;

    void Update()
    {
        if (!FollowTarget) return;
        SpringCalc();
        if (AjustParentRotation) AjustRotation();
    }

    private void SpringCalc() 
    {
        Vector3 force = Target.transform.position - transform.position;

        force *= strength;

        velocity *= drag;
        velocity += force;

        transform.position += velocity;
    }

    private void AjustRotation() 
    {
        transform.rotation = Target.transform.rotation;
    }

    public void SetTarget(GameObject target) 
    {
        this.Target = target;
    }

    public GameObject GetTarget() 
    {
        return this.Target;
    }

    public void SetStrength(float strength) 
    {
        this.strength = strength;
    }

    public float GetStrength() 
    {
        return this.strength;
    }

    public void SetDrag(float drag) 
    {
        this.drag = drag;
    }

    public float GetDrag() 
    {
        return this.drag;
    }

}
