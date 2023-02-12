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

public class ProgrammingManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private TextAsset textJSON;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject gameWon;
    [SerializeField] private ProgrammingArea programmingArea;

    private TMP_LinkInfo[] links;
    public static ProgrammingManager instance { get; private set; }
    private int taskLevel = 1;
    private TasksList tasksList = new TasksList();
    private GameData.Level[] levels;
    private int level;

    public void LoadData(GameData data)
    {
        this.level = data.level;
        this.levels = data.levels;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }

    private void Start()
    {
        if (instance != null) { Destroy(this.gameObject); }
        else { instance = this; }

        tasksList = JsonUtility.FromJson<TasksList>(textJSON.text);
        Shuffle();

        programmingArea.InitProgrammingArea();
    }
    public void SetLinks(TMP_LinkInfo[] _links)
    {
        links = _links;
        CheckResults();
    }

    private void CheckResults()
    {
        for (int i = 0; i < tasksList.tasks[taskLevel - 1].results.Length; i++)
        {
            Debug.Log($"{tasksList.tasks[taskLevel - 1].results[i]} | {links[i].GetLinkText()} | {tasksList.tasks[taskLevel - 1].results[i] == links[i].GetLinkText()}");
            if (tasksList.tasks[taskLevel - 1].results[i] != links[i].GetLinkText())
                return;
        }
        timer.gameObject.SetActive(false);
        FunctionTimer.Create(GameWon, 3f);
    }

    public string GetCode()
    {
        return tasksList.tasks[taskLevel - 1].code.Replace("<newLine>", System.Environment.NewLine);
    }

    private void Shuffle()
    {
        for (int i = 0; i < tasksList.tasks.Length; i++)
        {
            int rnd = Random.Range(0, tasksList.tasks.Length);
            Task tempGO = tasksList.tasks[rnd];
            tasksList.tasks[rnd] = tasksList.tasks[i];
            tasksList.tasks[i] = tempGO;
        }
    }
    private void GameWon()
    {
        if (taskLevel == 2)
        {
            gameWon.gameObject.SetActive(true);
            Time.timeScale = 0f;
            DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Programming);
        }
        else
        {
            timer.gameObject.SetActive(true);
            taskLevel++;
            programmingArea.InitProgrammingArea();
        }
    }
}