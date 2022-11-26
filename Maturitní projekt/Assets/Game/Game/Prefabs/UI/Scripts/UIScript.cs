using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDisplay;

    void Start()
    {
        levelDisplay.GetComponent<TextMeshProUGUI>().text = "Level:" + GameManager.instance.GetLevel().ToString();
    }
}
