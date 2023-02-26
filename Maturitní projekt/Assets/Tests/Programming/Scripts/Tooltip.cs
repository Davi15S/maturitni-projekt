using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private RectTransform rt;
    private float height;
    private Image image;
    private TooltipBackground tooltipBackground;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        tooltipBackground = GetComponentInChildren<TooltipBackground>();
    }

    public void SetHeight(float height, float width)
    {
        image.rectTransform.sizeDelta = new Vector2(width + 10, height + 20);
        rt.sizeDelta = new Vector2(width + 10, height + 20);
        tooltipBackground.SetDimentions(width + 10, height);
    }

    public float GetHeight()
    {
        return rt.rect.height;
    }
}
