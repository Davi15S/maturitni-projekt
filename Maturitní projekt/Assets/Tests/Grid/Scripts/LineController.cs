using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Cable cursor;

    private void Awake()
    {
        cursor = GetComponentInParent<Cable>();
        lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(3);
    }

    private void Update()
    {
        lr.positionCount = cursor.GetPointsList().Count;
        for (int i = 0; i < cursor.GetPointsList().Count; i++)
        {
            lr.SetPosition(i, cursor.GetPointsList()[i]);
        }
    }
}
