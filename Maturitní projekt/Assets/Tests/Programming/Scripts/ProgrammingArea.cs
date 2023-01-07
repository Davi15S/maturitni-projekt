using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

public class ProgrammingArea : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject buttonParent;
    private string initText;
    private Canvas canvas;
    private TextMeshProUGUI text;
    private string[] texts;
    private List<ProgramItem> programItems;

    private RectTransform textBoxRectTransform;
    private int currentlyActiveLinkedElement;

    private TooltipHandler tooltip;
    public delegate void CloseTooltipEvent();
    public static event CloseTooltipEvent OnCloseTooltipEvent;

    public delegate void HoverOnLintEvent(string keyword, Vector3 mousePos, string linkText);
    public static event HoverOnLintEvent OnHoverLinkEvent;

    void Awake()
    {
        initText = GetComponentInParent<Programming>().initText;
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        text.text = initText;
        text.ForceMeshUpdate();
        tooltip = GetComponentInParent<TooltipHandler>();
        programItems = GetComponentInParent<Programming>().programItems;


        textBoxRectTransform = GetComponentInChildren<RectTransform>();
    }

    void Start()
    {
        char[] seperators = new char[] { ' ' };
        texts = initText.Replace("\n", "Đ ").Replace("(", "( ").Replace(")", " )").Split(seperators, StringSplitOptions.RemoveEmptyEntries);

        foreach (ProgramItem item in programItems)
        {
            foreach (string word in item.replaceWords)
            {
                InitProgramming(word);
            }
        }
    }

    void Update()
    {
        CheckForLinkAtMousePosition();
    }

    private void InitProgramming(string findWord)
    {
        int[] indexes = texts.Select((item, i) => item == findWord ? i : -1).Where(i => i != -1).ToArray();

        foreach (int i in indexes)
        {
            texts[i] = $"<link=\"{findWord}{i}\"><style=\"Program\">{findWord}</style></link>";
        }

        text.text = string.Join(" ", texts).Replace("Đ ", "\n").Replace("Đ", "").Replace("<tab>", "    ").Replace("( ", "(").Replace(" )", ")");
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(textBoxRectTransform, mousePos, null);
        if (!isIntersectingRectTransform)
            return;

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(text, mousePos, null);

        if (currentlyActiveLinkedElement != intersectingLink && !tooltip.MouseOver())
            OnCloseTooltipEvent?.Invoke();

        if (intersectingLink == -1)
            return;

        TMP_LinkInfo linkInfo = text.textInfo.linkInfo[intersectingLink];

        OnHoverLinkEvent?.Invoke(linkInfo.GetLinkID(), mousePos, linkInfo.GetLinkText());
        currentlyActiveLinkedElement = intersectingLink;
    }
}
