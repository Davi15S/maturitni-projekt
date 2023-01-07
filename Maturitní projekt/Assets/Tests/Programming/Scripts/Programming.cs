using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Programming : MonoBehaviour
{
    [TextArea(5, 20)]
    [SerializeField] public string initText;
    [SerializeField] private GameObject tooltipContainer;
    [SerializeField] public List<ProgramItem> programItems;
    [SerializeField] private TooltipButton tooltipButton;
    [SerializeField] private Tooltip tooltip;
    private void OnEnable()
    {
        ProgrammingArea.OnHoverLinkEvent += GetTooltipInfo;
        ProgrammingArea.OnCloseTooltipEvent += CloseTooltip;
    }

    private void OnDisable()
    {
        ProgrammingArea.OnHoverLinkEvent -= GetTooltipInfo;
        ProgrammingArea.OnCloseTooltipEvent -= CloseTooltip;
    }

    private void GetTooltipInfo(string linkId, Vector3 mousePos, string linkText)
    {
        foreach (ProgramItem item in programItems)
        {
            if (item.replaceWords.Contains(linkText))
            {
                if (!tooltipContainer.gameObject.activeInHierarchy)
                {
                    tooltipContainer.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                    tooltipButton.InstantiateButtons(item.replaceWords, linkText, linkId);
                    tooltipContainer.SetActive(true);
                }
                return;
            }
        }
    }

    private void CloseTooltip()
    {
        if (tooltipContainer.gameObject.activeInHierarchy)
        {
            tooltipContainer.SetActive(false);
            tooltipButton.DestroyChildrens();
        }
    }
}
