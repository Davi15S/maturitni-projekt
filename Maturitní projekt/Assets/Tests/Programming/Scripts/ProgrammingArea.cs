using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

public class ProgrammingArea : MonoBehaviour
{
    private string initText;
    private Canvas canvas;
    private TextMeshProUGUI text;
    private string[] texts;
    private List<ProgramItem> programItems;
    private Tooltip tooltip;

    private RectTransform textBoxRectTransform;
    private int currentlyActiveLinkedElement;

    private TooltipHandler tooltipHandler;
    public delegate void CloseTooltipEvent();
    public static event CloseTooltipEvent OnCloseTooltipEvent;

    public delegate void HoverOnLintEvent(string keyword, Vector3 mousePos, string linkText);
    public static event HoverOnLintEvent OnHoverLinkEvent;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        tooltipHandler = GetComponentInParent<TooltipHandler>();
        programItems = GetComponentInParent<Programming>().programItems;

        tooltip = GetComponentInChildren<Tooltip>();
        textBoxRectTransform = GetComponentInChildren<RectTransform>();
    }

    void Start()
    {
        initText = ProgrammingManager.instance.GetLevel().code.Replace("<newLine>", System.Environment.NewLine);
        text.text = initText;
        text.ForceMeshUpdate();

        char[] seperators = new char[] { ' ' };
        texts = initText.Replace("\n", "Đ ").Replace("(", "( ").Replace(")", " )").Split(seperators, StringSplitOptions.RemoveEmptyEntries);

        foreach (ProgramItem item in programItems)
        {
            foreach (string word in item.replaceWords)
            {
                InitProgramming(word);
            }
        }
        text.ForceMeshUpdate();
        ProgrammingManager.instance.SetLinks(text.textInfo.linkInfo);
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
            texts[i] = $"<style=\"Program\"><link=\"{findWord}{i}\">{findWord}</link></style>";
        }
        text.text = string.Join(" ", texts).Replace("Đ ", "\n").Replace("Đ", "").Replace("<tab>", "    ").Replace("( ", "(").Replace(" )", ")");
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(textBoxRectTransform, mousePos, null);
        if (!isIntersectingRectTransform)
            return;

        int intersectingWord = TMP_TextUtilities.FindIntersectingWord(text, mousePos, null);
        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(text, mousePos, null);

        if (currentlyActiveLinkedElement != intersectingLink && !tooltipHandler.MouseOver())
            OnCloseTooltipEvent?.Invoke();

        if (intersectingLink == -1)
            return;

        TMP_LinkInfo linkInfo = text.textInfo.linkInfo[intersectingLink];

        int firstcharacterIndex = text.textInfo.wordInfo[intersectingWord].firstCharacterIndex;

        Vector3 bottomLeft = text.textInfo.characterInfo[firstcharacterIndex].bottomLeft;
        Vector3 bottomRight = text.textInfo.characterInfo[firstcharacterIndex].bottomRight;

        Vector3 worldBottomLeft = text.transform.TransformPoint(bottomLeft);
        Vector3 worldBottomRight = text.transform.TransformPoint(bottomRight);


        OnHoverLinkEvent?.Invoke(linkInfo.GetLinkID(), (worldBottomLeft + worldBottomRight) / 2, linkInfo.GetLinkText());
        currentlyActiveLinkedElement = intersectingLink;
    }

    public void SetWord(string linkId, string replaceWord)
    {
        string currentLinkText = "";
        foreach (TMP_LinkInfo item in text.textInfo.linkInfo)
        {
            if (item.GetLinkID() == linkId)
            {
                currentLinkText = item.GetLinkText();
            }
        }

        int index = text.text.IndexOf(linkId);
        string str = text.text.Substring(index, (currentLinkText.Length + linkId.Length + 3));
        string[] arr = text.text.Split(str);
        string replacedWord = str.Replace(currentLinkText, replaceWord);
        text.text = string.Join(replacedWord, arr);

        tooltip.GetComponent<Transform>().gameObject.SetActive(false);
        tooltip.GetComponentInChildren<TooltipButton>().DestroyChildrens();
        text.ForceMeshUpdate();
        ProgrammingManager.instance.SetLinks(text.textInfo.linkInfo);
    }
}
