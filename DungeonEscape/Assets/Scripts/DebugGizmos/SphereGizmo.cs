using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGizmo : MonoBehaviour
{
    public float size = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 1);
        Gizmos.DrawWireSphere(transform.position, size);

        Gizmos.color = new Color(0, 0, 1, 1);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward*size);

        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * size);

        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * size);
    }
}
