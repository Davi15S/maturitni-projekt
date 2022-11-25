using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public enum MathOperations { ADD, SUBSTRACT, MULTIPLY, DIVIDE }

public class GeneratedNumberManager : MonoBehaviour
{
    public static GeneratedNumberManager instance { get; private set; }
    [SerializeField] private GameObject displayNumber;
    [SerializeField] private GameObject totalNumber;
    [SerializeField] private GameObject cables;
    [SerializeField] private GameObject goals;

    private GenerateCableNumber[] cableNumbers;
    private GoalScript[] goalNumbers;
    private int generatedNumber;

    private float calculatedTotalNumber;
    private float resultNumber;

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
        cableNumbers = cables.GetComponentsInChildren<GenerateCableNumber>();
        goalNumbers = goals.GetComponentsInChildren<GoalScript>();

        CalculateResultNumber();

        displayNumber.GetComponent<TextMeshProUGUI>().text = resultNumber.ToString();
        totalNumber.GetComponent<TextMeshProUGUI>().text = "0";
    }

    public void CalculateTotalNumber(float number, bool substact)
    {
        if (substact) { calculatedTotalNumber -= RoundNumber(number); }
        else { calculatedTotalNumber += RoundNumber(number); }
        calculatedTotalNumber = RoundNumber(calculatedTotalNumber);
        totalNumber.GetComponent<TextMeshProUGUI>().text = calculatedTotalNumber.ToString();
        if (calculatedTotalNumber == resultNumber) { Debug.Log("Výhra"); }
    }
    private void CalculateResultNumber()
    {
        cableNumbers = cableNumbers.OrderBy(i => Random.value).ToArray();
        for (int i = 0; i < cableNumbers.Length; i++)
        {
            Debug.Log(cableNumbers[i].GetGeneratedNumber() + "|" + goalNumbers[i].GetGeneratedNumber() + "|" + goalNumbers[i].GetMathOperation());
            resultNumber += CalculateNumber(cableNumbers[i].GetGeneratedNumber(), goalNumbers[i].GetGeneratedNumber(), goalNumbers[i].GetMathOperation());
        }
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
}
