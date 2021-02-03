using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrigger : MonoBehaviour
{
    public delegate void TriggerEnter(Collider Other, Collider selfe);
    public event TriggerEnter IOnTriggerEnter;
    public delegate void TriggerExit(Collider Other, Collider selfe);
    public event TriggerExit IOnTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        IOnTriggerEnter?.Invoke(other, transform.gameObject.GetComponent<Collider>());
    }

    private void OnTriggerExit(Collider other)
    {
        IOnTriggerExit?.Invoke(other, transform.gameObject.GetComponent<Collider>());
    }

}
