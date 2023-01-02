using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ProgrammingArea : MonoBehaviour
{
    private string initText;
    private TextMeshProUGUI text;
    private string[] texts;
    private Rect target = new Rect();
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject buttonParent;

    private string mainColor = "#1049a3";

    void Awake()
    {
        initText = GetComponentInParent<Programming>().initText;
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = initText;
        text.ForceMeshUpdate();
    }

    void Start()
    {
        InitProgramming();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitProgramming()
    {
        string findWord = "for";

        char[] seperators = new char[] { ' ' };
        texts = initText.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

        int index = Array.IndexOf(texts, findWord);
        texts[index] = "<link><style=\"Program\">" + findWord + "</style></link>";

        text.text = string.Join(" ", texts);
    }
}
