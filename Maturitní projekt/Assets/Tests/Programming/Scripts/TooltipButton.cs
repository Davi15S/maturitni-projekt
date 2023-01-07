using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipButton : MonoBehaviour
{
    RectTransform rc;
    [SerializeField] private Button button;
    private Tooltip tooltip;
    private Button[] childrens;
    private float height;
    private float width;

    void Awake()
    {
        rc = GetComponent<RectTransform>();
        tooltip = GetComponentInParent<Tooltip>();
    }

    public void InstantiateButtons(List<string> words, string currentLinkText, string linkId)
    {
        if (transform.childCount < words.Count - 1)
        {
            foreach (string word in words)
            {
                if (word != currentLinkText)
                {
                    Button obstacle = Instantiate(button);
                    obstacle.transform.SetParent(this.transform);
                    obstacle.GetComponentInChildren<TextMeshProUGUI>().text = word;
                    obstacle.GetComponent<ProgrammingButton>().SetLinkId(linkId);
                    height += obstacle.GetComponent<RectTransform>().rect.height + 5;
                    width = obstacle.GetComponent<RectTransform>().rect.width;
                }
            }
        }

        tooltip.SetHeight(height, width);
    }

    public void DestroyChildrens()
    {
        childrens = GetComponentsInChildren<Button>();
        foreach (Button item in childrens)
        {
            DestroyImmediate(item.gameObject);
        }
        height = 0;
    }
}
