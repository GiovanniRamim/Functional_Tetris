using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Cell : MonoBehaviour
{
    [SerializeField] Vector3 Gizmos_BoxSize = Vector3.one;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (GetComponentInParent<ObjectGrid3>().debugMode)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, this.GetComponentInParent<ObjectGrid3>().CellLocalScale);
        }
    }


}
