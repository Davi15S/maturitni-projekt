using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum LogicGateType { AND, OR, XOR, NOR }

[CreateAssetMenu(fileName = "LogicGate", menuName = "LogicGate")]
public class LogicGateObject : ScriptableObject
{
    public LogicGateType type;
    public Sprite sprite;

    public bool CheckConnection(InputScript[] array, LogicGateType logicGateType)
    {
        if (logicGateType == LogicGateType.AND) { return array.All(x => x.GetConnection()); }
        else if (logicGateType == LogicGateType.OR) { return array.Any(x => x.GetConnection()); }
        else if (logicGateType == LogicGateType.XOR) { return array[0].GetConnection() ^ array[1].GetConnection(); }
        else if (logicGateType == LogicGateType.NOR) { return array.All(x => !x.GetConnection()); }
        else { return false; }
    }
}
