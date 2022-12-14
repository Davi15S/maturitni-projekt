using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    [SerializeField] private List<LogicGateObject> logicGateObjList;
    [SerializeField] private Cable output;
    private LogicGateObject logicGateObj;
    private LogicGateType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private InputScript[] inputScriptArray;

    void Start()
    {
        SetLogicGate(RoboticsManager.instance.GetLogicGateType());
        inputScriptArray = GetComponentsInChildren<InputScript>();
        spriteRenderer.color = logicGateObj.color;
        type = logicGateObj.type;
    }

    void Update()
    {
        bool connection = logicGateObj.CheckConnection(inputScriptArray, type);
        output.SetConnection(connection, type);
    }

    private void SetLogicGate(LogicGateType type)
    {
        logicGateObj = logicGateObjList.Find(x => x.type == type);
    }
}
