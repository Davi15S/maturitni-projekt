using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Cursor cursor;

    private void Awake()
    {
        cursor = GetComponentInParent<Cursor>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.positionCount = cursor.GetPointsList().Count;
        for (int i = 0; i < cursor.GetPointsList().Count; i++) { lr.SetPosition(i, cursor.GetPointsList()[i]); }
    }
}
