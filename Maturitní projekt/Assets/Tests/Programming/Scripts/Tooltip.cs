using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private RectTransform rt;
    private float height;
    private Image image;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void SetHeight(float height, float width)
    {
        Debug.Log(width);
        image.rectTransform.sizeDelta = new Vector2(width, height + 20);
        rt.sizeDelta = new Vector2(width, height + 20);
    }

    public float GetHeight()
    {
        return rt.rect.height;
    }
}
