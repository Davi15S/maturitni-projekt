using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratedNumberManager : MonoBehaviour
{
    private enum MathOperations { ADD, SUBSTRACT, MULTIPLY, DIVIDE }

    [SerializeField] private GameObject displayNumber;
    public static GeneratedNumberManager instance { get; private set; }
    private int generatedNumber;

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
        generatedNumber = GenerateNumber();
        displayNumber.GetComponent<TextMeshProUGUI>().text = generatedNumber.ToString();
    }

    private int GenerateNumber()
    {
        return Random.Range(0, 1001);
    }
}
