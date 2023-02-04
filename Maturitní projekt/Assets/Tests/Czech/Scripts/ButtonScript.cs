using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private string text;
    private int index;
    void Start()
    {
        text = this.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void CheckAnswer()
    {
        CzechManager.instance.CheckAnswer(index);
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}
