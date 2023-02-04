using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject goals;
    private InputScript[] inputs;
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
            GameWon();
        }
    }
    private void GameWon()
    {
        gameFinished = true;
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Robotics);
    }
}
