using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreaEntered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CharControll>().DisableControlles();
    }
}
