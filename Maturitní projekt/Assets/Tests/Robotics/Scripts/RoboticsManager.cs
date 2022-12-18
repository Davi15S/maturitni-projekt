using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoboticsManager : MonoBehaviour
{
    [SerializeField] private GameObject goals;
    private InputScript[] inputs;
    public static RoboticsManager instance { get; private set; }
    private LogicGateType type = LogicGateType.AND;
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
        if (inputs.All(x => x.GetConnection())) { Debug.Log("Vyhrál jsi"); }
    }
}
