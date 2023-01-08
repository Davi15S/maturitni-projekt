using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgrammingButton : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private string linkId;
    private TextMeshProUGUI buttonText;
    private ProgrammingArea programmingArea;

    void Start()
    {
        textMeshPro = GetComponentInParent<TextMeshProUGUI>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        programmingArea = GetComponentInParent<ProgrammingArea>();
    }

    public void SetWord()
    {
        programmingArea.SetWord(linkId, buttonText.text);
    }

    public void SetLinkId(string link)
    {
        linkId = link;
    }
}
