using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPStairsScript : MonoBehaviour
{
    public GameObject Destination;
    public bool TopActive = true;

    private Vector3 offset;
    private bool canTP = true;

    private TPStairsScript ToScript;

    private GameObject ActiveTrigger;

    private void Awake()
    {
        this.ToScript = Destination.GetComponent<TPStairsScript>();
        this.ActiveTrigger = transform.Find((TopActive ? "TriggerTop" : "TriggerBot")).gameObject;
        ActiveTrigger.GetComponent<SimpleTrigger>().IOnTriggerEnter += TiggerEnter;
        ActiveTrigger.GetComponent<SimpleTrigger>().IOnTriggerExit += TriggerExit;
    }

    public void TiggerEnter(Collider other, Collider selfe) 
    {
        if (canTP && other.gameObject.tag.Equals("Player") && selfe.gameObject.Equals(ActiveTrigger))
        {
            this.ToScript.canTP = false;
            Vector3 newPos = ToScript.ActiveTrigger.transform.position - other.transform.position;
            other.gameObject.GetComponent<CharacterController>().Move(newPos);
            other.gameObject.transform.rotation = Quaternion.LookRotation(selfe.transform.right, selfe.transform.up);
        } else if (!canTP && other.gameObject.tag.Equals("Player") && !selfe.gameObject.Equals(ActiveTrigger)) 
        {
            this.canTP = true;
        }
    }

    public void TriggerExit(Collider other, Collider selfe)
    {
        if (!canTP && other.gameObject.tag.Equals("Player"))
        {
            this.canTP = true;
        }
    }

}
