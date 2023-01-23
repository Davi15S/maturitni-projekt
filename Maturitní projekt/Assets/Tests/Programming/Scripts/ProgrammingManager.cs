using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Task
{
    public string code;
    public string[] results;
}

[System.Serializable]
public class TasksList
{
    public Task[] tasks;
}

public class ProgrammingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private TMP_LinkInfo[] links;
    public static ProgrammingManager instance { get; private set; }
    private int intLevel = 1;
    [SerializeField] private TextAsset textJSON;
    private TasksList tasksList = new TasksList();

    private void Start()
    {
        if (instance != null) { Destroy(this.gameObject); }
        else { instance = this; }

        tasksList = JsonUtility.FromJson<TasksList>(textJSON.text);
    }
    public void SetLinks(TMP_LinkInfo[] _links)
    {
        links = _links;
        CheckResults();
    }

    private void CheckResults()
    {
        for (int i = 0; i < tasksList.tasks[0].results.Length; i++)
        {
            Debug.Log($"{tasksList.tasks[0].results[i]} | {links[i].GetLinkText()} | {tasksList.tasks[0].results[i] == links[i].GetLinkText()}");
            if (tasksList.tasks[0].results[i] != links[i].GetLinkText())
                return;
        }
        Debug.LogWarning("Vyhrál jsi!");
    }

    public string GetCode()
    {
        return tasksList.tasks[0].code.Replace("<newLine>", System.Environment.NewLine);
    }
}
