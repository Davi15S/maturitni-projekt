using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGateButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private LogicGateType type;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetLogicGateType);
    }

    public LogicGateType GetLogicGateType() { return type; }

    public void SetLogicGateType() { RoboticsManager.instance.SetLogicGateType(type); }
}
