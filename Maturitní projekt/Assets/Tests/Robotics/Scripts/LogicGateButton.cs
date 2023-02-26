using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGateButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private LogicGateType type;
    private Image image;
    [SerializeField] private Sprite activeSprite;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetLogicGateType);
        image = GetComponent<Image>();
    }

    public LogicGateType GetLogicGateType() { return type; }

    public void SetLogicGateType()
    {
        RoboticsManager.instance.SetLogicGateType(type);
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
