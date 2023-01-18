using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgrammingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private TMP_LinkInfo[] links;
    public static ProgrammingManager instance { get; private set; }
    private ProgrammingData programmingData;
    private int intLevel = 1;
    private ProgrammingData.Level level;

    private void Start()
    {
        if (instance != null) { Destroy(this.gameObject); }
        else { instance = this; }

        programmingData = new ProgrammingData();
        level = programmingData.levels.Find(item => item.level == intLevel);
    }
    public void SetLinks(TMP_LinkInfo[] _links)
    {
        links = _links;
        CheckResults();
    }

    private void CheckResults()
    {
        for (int i = 0; i < level.results.Length; i++)
        {
            Debug.Log($"{level.results[i]} | {links[i].GetLinkText()} | {level.results[i] == links[i].GetLinkText()}");
            if (level.results[i] != links[i].GetLinkText())
                return;
        }
        Debug.LogWarning("Vyhrál jsi!");
    }

    public ProgrammingData.Level GetLevel()
    {
        return level;
    }
}
