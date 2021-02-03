using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectableHolder : MonoBehaviour
{
    public GameObject[] Places;

    public GameObject[] Collectables;

    private int current = 0;

    private void Awake()
    {
        foreach (GameObject obj in Collectables) 
        {
            obj.GetComponent<Collectable>().OnCollect += CollectShield;
        }
    }

    public int getCurrent() 
    {
        return current;
    }

    public void CollectShield(GameObject obj) 
    {
        if (current >= Places.Length) return;
        obj.GetComponent<Collectable>().Animate = false;
        obj.transform.SetPositionAndRotation(Places[current].transform.position, Quaternion.LookRotation(Places[current].transform.forward, Places[current].transform.up));
        current++;
    }

    public bool hasFreePlace() 
    {
        return current < Places.Length;
    }

}
