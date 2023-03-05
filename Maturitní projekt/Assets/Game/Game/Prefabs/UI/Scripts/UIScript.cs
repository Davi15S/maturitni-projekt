using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Image levelDisplay;
    [SerializeField] private List<Sprite> levelSprites;

    void Start()
    {
        levelDisplay.GetComponent<Image>().sprite = levelSprites[GameManager.instance.GetLevel() - 1];
    }
}
