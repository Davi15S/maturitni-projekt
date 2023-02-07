using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject goals;
    [SerializeField] private GameObject cablesObj;
    [SerializeField] private GameObject gameWon;
    [SerializeField] private Timer timer;
    private InputScript[] inputs;
    private Cable[] cables;
    public static RoboticsManager instance { get; private set; }
    private LogicGateType type = LogicGateType.AND;
    private GameData.Level[] levels;
    private int level;
    private bool gameFinished = false;
    public void LoadData(GameData data)
    {
        this.levels = data.levels;
        this.level = data.level;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        cables = cablesObj.GetComponentsInChildren<Cable>();
    }

    void Update()
    {
        CheckGoalsConnection();
    }

    public LogicGateType GetLogicGateType() { return type; }
    public void SetLogicGateType(LogicGateType type) { this.type = type; }

    public void CheckGoalsConnection()
    {
        inputs = goals.GetComponentsInChildren<InputScript>();
        if (inputs.All(x => x.GetConnection()) && !gameFinished)
        {
            gameFinished = true;
            timer.gameObject.SetActive(false);
            for (int i = 0; i < cables.Length; i++)
            {
                cables[i].SetIsDragable(false);
            }
            FunctionTimer.Create(GameWon, 3f);
        }
    }
    private void GameWon()
    {
        gameWon.gameObject.SetActive(true);
        gameFinished = true;
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Robotics);
    }
}
