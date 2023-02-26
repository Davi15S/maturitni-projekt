using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipBackground : MonoBehaviour
{
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void SetDimentions(float width, float height)
    {
        rt.sizeDelta = new Vector2(width, height);
    }
}
