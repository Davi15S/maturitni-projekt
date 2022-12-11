using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    [SerializeField] private List<LogicGateObject> logicGateObjList;
    private LogicGateObject logicGateObj;
    private LogicGateType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private InputScript[] inputScriptArray;
    private Cable output;

    void Start()
    {
        SetLogicGate(RoboticsManager.instance.GetLogicGateType());
        inputScriptArray = GetComponentsInChildren<InputScript>();
        output = GetComponentInChildren<Cable>();
        spriteRenderer.color = logicGateObj.color;
        type = logicGateObj.type;
    }

    void Update()
    {
        output.SetConnection(logicGateObj.CheckConnection(inputScriptArray));
    }

    private void SetLogicGate(LogicGateType type)
    {
        logicGateObj = logicGateObjList.Find(x => x.type == type);
    }
}
