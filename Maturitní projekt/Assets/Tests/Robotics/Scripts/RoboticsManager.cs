using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticsManager : MonoBehaviour
{
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

    public LogicGateType GetLogicGateType() { return type; }
    public void SetLogicGateType(LogicGateType type) { this.type = type; }
}
