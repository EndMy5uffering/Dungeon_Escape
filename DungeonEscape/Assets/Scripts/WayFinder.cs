using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFinder : MonoBehaviour
{

    public GameObject[] Targets;
    public GameObject Arrow;

    private GameObject currentTarget;

    public float TargetDist = 50;

    public float ArrowHightOffset = 0;
    public float playerDistMultiplayer = 1f;

    private bool ArrowEnabled = false;
    public bool EnableWayFind = true;

    private void Awake()
    {
        Collectable.OnCollect += Collected;
        if (!EnableWayFind) 
        {
            Arrow.SetActive(false);
        }
    }

    void Update()
    {
        if (!EnableWayFind || Targets.Length <= 0) return;

        if (ArrowEnabled == false || currentTarget == null) 
        {
            currentTarget = FindNextTarget();
            if (currentTarget != null)
            {
                ArrowEnabled = true;
                Arrow.SetActive(true);
            }
            else 
            {
                Arrow.SetActive(false);
            }
        }

        if (TargetDistance(currentTarget) > TargetDist) 
        {
            currentTarget = FindNextTarget();
        }

        if (currentTarget != null)
        {
            Vector3 dir = GetVectorToTarget(currentTarget).normalized;
            dir.y = transform.position.y + ArrowHightOffset;
            RotateArrowToTarget(dir);
        }
        else 
        {
            ArrowEnabled = false;
            Arrow.SetActive(false);
        }

    }

    private GameObject FindNextTarget() 
    {
        if (currentTarget != null && TargetDistance(currentTarget) <= TargetDist) return currentTarget;

        GameObject Closest = Targets[0];
        float closestDist = TargetDistance(Closest);
        foreach (GameObject o in Targets) 
        {
            float dist = TargetDistance(o);
            if (dist < closestDist) 
            {
                Closest = o;
                closestDist = dist;
            }
        }
        if (TargetDistance(Closest) > TargetDist) return null;
        return Closest;
    }

    private Vector3 GetVectorToTarget(GameObject obj) 
    {
        if (obj == null) return new Vector3(-1, -1, -1);
        return currentTarget.transform.position - transform.position;
    }

    private void RotateArrowToTarget(Vector3 dir) 
    {
        Arrow.transform.SetPositionAndRotation(transform.position + (dir * playerDistMultiplayer), Quaternion.LookRotation(new Vector3(0,1,0), dir));
    }

    private float TargetDistance(GameObject obj) 
    {
        if (obj == null) return -1;
        return (obj.transform.position - transform.position).magnitude;
    }

    private GameObject[] removeObjectFromList(GameObject obj) 
    {
        GameObject[] temp = new GameObject[Targets.Length-1];

        int c = 0;
        foreach (GameObject o in Targets) 
        {
            if(!o.Equals(obj)) temp[c++] = o; 
        }

        return temp;
    }

    public void Collected(GameObject obj) 
    {
        Targets = removeObjectFromList(obj);
        this.currentTarget = null;
    }

}
