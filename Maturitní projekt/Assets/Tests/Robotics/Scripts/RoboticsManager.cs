using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject goals;
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject buttons;

    [SerializeField] private List<Sprite> defaultSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> activeSprites = new List<Sprite>();

    private LogicGateButton[] logicGateButtons;
    private InputScript[] inputs;
    public static RoboticsManager instance { get; private set; }
    private LogicGateType type;
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

        logicGateButtons = buttons.GetComponentsInChildren<LogicGateButton>();
        SetLogicGateType(LogicGateType.AND);
    }

    void Update()
    {
        CheckGoalsConnection();
    }

    public LogicGateType GetLogicGateType() { return type; }
    public void SetLogicGateType(LogicGateType type)
    {
        this.type = type;

        for (int i = 0; i < logicGateButtons.Length; i++)
        {
            if (logicGateButtons[i].GetLogicGateType() == type) { logicGateButtons[i].SetSprite(activeSprites[i]); }
            else { logicGateButtons[i].SetSprite(defaultSprites[i]); }
        }
    }

    public void CheckGoalsConnection()
    {
        inputs = goals.GetComponentsInChildren<InputScript>();
        if (inputs.All(x => x.GetConnection()) && !gameFinished)
        {
            timer.StopTimer();
            gameFinished = true;
            FunctionTimer.Create(GameWon, 3f);
        }
    }
    private void GameWon()
    {
        gameWonCanvas.SetActive(true);
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Robotics);
    }
}
