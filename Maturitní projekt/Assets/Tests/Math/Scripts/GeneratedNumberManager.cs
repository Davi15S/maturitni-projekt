using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public enum MathOperations { ADD, SUBSTRACT, MULTIPLY, DIVIDE }

public class GeneratedNumberManager : MonoBehaviour, IDataPersistence
{
    public static GeneratedNumberManager instance { get; private set; }

    [SerializeField] private GameObject displayNumber;
    [SerializeField] private GameObject totalNumber;
    [SerializeField] private GameObject cablesObj;
    [SerializeField] private GameObject goalsObj;
    [SerializeField] private GameObject debugText;

    private GenerateCableNumber[] cables;
    private GoalScript[] goals;
    private GameData.Level[] levels;
    private int level;

    private float calculatedTotalNumber;
    private float resultNumber;

    public void LoadData(GameData data)
    {
        this.levels = data.levels;
        this.level = data.level;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        cables = cablesObj.GetComponentsInChildren<GenerateCableNumber>();
        goals = goalsObj.GetComponentsInChildren<GoalScript>();
        CalculateResultNumber();
        displayNumber.GetComponent<TextMeshProUGUI>().text = resultNumber.ToString();
        totalNumber.GetComponent<TextMeshProUGUI>().text = "0";
    }

    // Spočítá součet všech propojených kabelů
    public void CalculateTotalNumber(float number, bool substact)
    {
        if (substact) { calculatedTotalNumber -= RoundNumber(number); }
        else { calculatedTotalNumber += RoundNumber(number); }
        calculatedTotalNumber = RoundNumber(calculatedTotalNumber);
        totalNumber.GetComponent<TextMeshProUGUI>().text = calculatedTotalNumber.ToString();

        if (calculatedTotalNumber == resultNumber)
        {
            GameWon();
        }
    }

    // Spočítá výsledek, ke kterýmu se musí uživatel dostat
    private void CalculateResultNumber()
    {
        debugText.GetComponent<TextMeshProUGUI>().text = "Initing quiz!";
        cables = cables.OrderBy(i => Random.value).ToArray();
        for (int i = 0; i < cables.Length; i++)
        {
            // Debug text
            Debug.Log(cables[i].GetGeneratedNumber() + "|" + goals[i].GetGeneratedNumber() + "|" + goals[i].GetMathOperation());

            resultNumber = resultNumber + CalculateNumber(cables[i].GetGeneratedNumber(), goals[i].GetGeneratedNumber(), goals[i].GetMathOperation());
            Debug.Log(cables[i].GetGeneratedNumber());
        }
        debugText.GetComponent<TextMeshProUGUI>().text += resultNumber;
        resultNumber = RoundNumber(resultNumber);
    }

    public float CalculateNumber(int cableNumber, int goalNumber, MathOperations mathOperation)
    {
        switch (mathOperation)
        {
            case MathOperations.ADD:
                return cableNumber + goalNumber;
            case MathOperations.SUBSTRACT:
                return cableNumber - goalNumber;
            case MathOperations.MULTIPLY:
                return cableNumber * goalNumber;
            case MathOperations.DIVIDE:
                return RoundNumber((float)cableNumber / (float)goalNumber);
            default:
                return 0;
        }
    }
    private float RoundNumber(float number) { return Mathf.Round(number * 100.0f) * 0.01f; }

    private void GameWon()
    {
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Math);
    }
}
