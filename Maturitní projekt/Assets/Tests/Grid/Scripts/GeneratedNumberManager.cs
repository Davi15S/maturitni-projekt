using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MathOperations { ADD, SUBSTRACT, MULTIPLY, DIVIDE }

public class GeneratedNumberManager : MonoBehaviour
{
    [SerializeField] private GameObject displayNumber;
    [SerializeField] private GameObject totalNumber;
    [SerializeField] private GameObject cables;
    [SerializeField] private GameObject goals;

    private GenerateCableNumber[] cableNumbers;
    private GoalScript[] goalNumbers;
    public static GeneratedNumberManager instance { get; private set; }

    private int generatedNumber;
    private MathOperations mathOperation;
    private int goalGeneratedNumber;

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

        displayNumber.GetComponent<TextMeshProUGUI>().text = generatedNumber.ToString();
        totalNumber.GetComponent<TextMeshProUGUI>().text = "0";
    }

    void Update()
    {
        totalNumber.GetComponent<TextMeshProUGUI>().text = calculatedTotalNumber.ToString();
    }

    public void CalculateTotalNumber(float number, bool substact)
    {
        if (substact) { calculatedTotalNumber -= RoundNumber(number); }
        else { calculatedTotalNumber += RoundNumber(number); }
        calculatedTotalNumber = RoundNumber(calculatedTotalNumber);
    }
    private void CalculateResultNumber()
    {

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
                Debug.Log(((float)cableNumber / (float)goalNumber));
                return RoundNumber((float)cableNumber / (float)goalNumber);
            default:
                return 0;
        }
    }

    private float RoundNumber(float number) { return Mathf.Round(number * 100.0f) * 0.01f; }
}
