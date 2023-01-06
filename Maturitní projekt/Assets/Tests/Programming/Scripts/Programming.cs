using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Programming : MonoBehaviour
{
    [TextArea(5, 20)]
    [SerializeField] public string initText;
    [SerializeField] private GameObject tooltipContainer;

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

    private void GetTooltipInfo(string keyword, Vector3 mousePos)
    {
        if (!tooltipContainer.gameObject.activeInHierarchy)
        {
            tooltipContainer.transform.position = new Vector3(mousePos.x, mousePos.y - 20, 0);
            tooltipContainer.SetActive(true);
        }
    }

    private void CloseTooltip()
    {
        if (tooltipContainer.gameObject.activeInHierarchy)
        {
            tooltipContainer.SetActive(false);
        }
    }
}
