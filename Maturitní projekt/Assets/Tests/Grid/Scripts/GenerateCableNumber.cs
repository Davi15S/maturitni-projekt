using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateCableNumber : MonoBehaviour
{
    private TextMeshPro displayNumber;
    private int generatedNumber;

    void Start()
    {
        GenerateNumber();
        GetComponentInChildren<Cable>().SetGeneratedNumber(generatedNumber);

        // Debug text
        displayNumber = GetComponentInChildren<TextMeshPro>();
        displayNumber.text = generatedNumber.ToString();
    }

    private void GenerateNumber() { generatedNumber = Random.Range(0, 11); }
    public int GetGeneratedNumber() { return generatedNumber; }
}
