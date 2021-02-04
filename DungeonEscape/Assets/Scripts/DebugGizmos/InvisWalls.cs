using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisWalls : MonoBehaviour
{
    public BoxCollider c;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(c.bounds.center, c.bounds.size);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(c.bounds.center, c.bounds.size);
        Gizmos.DrawWireCube(c.bounds.center, c.bounds.size);
    }
}
