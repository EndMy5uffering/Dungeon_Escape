using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectableHolder : MonoBehaviour
{
    public GameObject[] Places;

    public GameObject[] Collectables;

    public GameObject Door;

    private int current = 0;

    public delegate void Full();
    public static event Full OnFull;

    private void Awake()
    {
        Collectable.OnCollect += CollectShield;
    }

    public int getCurrent() 
    {
        return current;
    }

    public void CollectShield(GameObject obj) 
    {
        if (!obj.tag.Equals("Shield")) return;
        if (current >= Places.Length) return;
        obj.GetComponent<Collectable>().Animate = false;
        obj.transform.SetPositionAndRotation(Places[current].transform.position, Quaternion.LookRotation(Places[current].transform.forward, Places[current].transform.up));
        current++;
        if (!hasFreePlace()) 
        {
            OnFull?.Invoke();
            this.Door.GetComponent<Door>().DoorOpen();
        }
    }

    public bool hasFreePlace() 
    {
        return current < Places.Length;
    }

}
