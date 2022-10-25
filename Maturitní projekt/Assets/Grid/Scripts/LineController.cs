using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Cursor cursor;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.positionCount = cursor.getPointsList().Count;

        for (int i = 0; i < cursor.getPointsList().Count; i++)
        {
            lr.SetPosition(i, cursor.getPointsList()[i]);
        }
    }
}
