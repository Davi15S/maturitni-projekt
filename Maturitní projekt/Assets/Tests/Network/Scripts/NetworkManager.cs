using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NetworkManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject display;
    [SerializeField] private TextMeshProUGUI toBaseString;
    [SerializeField] private TextMeshProUGUI fromBaseString;
    private TextMeshProUGUI displayText;
    private int displayNumber;
    private string calculatedNumber;
    private string result;
    private int _base;
    private string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static NetworkManager instance { get; private set; }
    private bool isToDecimal;
    System.Random rn = new System.Random();
    private GameData.Level[] levels;
    private int level;

    public void LoadData(GameData data)
    {
        this.level = data.level;
        this.levels = data.levels;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        displayText = display.GetComponentInChildren<TextMeshProUGUI>();
        _base = rn.Next(2, 10);
        GenerateNumber();
        CalculategeneratedNumber();

        if (rn.Next(2) == 1)
        {
            toBaseString.text = $"Do {_base}kové soustavy!";
            displayText.text = displayNumber.ToString();
            isToDecimal = false;
            Debug.Log(result);
        }
        else
        {
            displayText.text = result;
            toBaseString.text = $"Do 10kové soustavy!";
            fromBaseString.text = $"Z {_base}kové soustavy!";
            isToDecimal = true;
            Debug.Log(displayNumber.ToString());
        }
    }

    void GenerateNumber()
    {
        displayNumber = rn.Next(0, 101);
    }

    void CalculategeneratedNumber()
    {
        string str = "";
        int i = displayNumber;
        int rest = 0;

        do
        {
            rest = i % _base;
            i = i / _base;
            str += chars[rest];
        } while (i != 0);

        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        result = new string(charArray);
    }

    public void CheckResult(string str)
    {
        if (isToDecimal)
        {
            if (displayNumber.ToString().Equals(str))
            {
                GameWon();
            }
        }
        else
        {
            if (result.Equals(str))
            {
                GameWon();
            }
        }
    }

    private void GameWon()
    {
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Network);
    }
}
