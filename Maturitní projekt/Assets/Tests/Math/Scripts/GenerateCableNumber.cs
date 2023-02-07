using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateCableNumber : MonoBehaviour
{
    private TextMeshPro displayNumber;
    private int generatedNumber;

    public void GenerateNumber()
    {
        generatedNumber = Random.Range(0, 11);
        GetComponentInChildren<Cable>().SetGeneratedNumber(generatedNumber);

        // Debug text
        displayNumber = GetComponentInChildren<TextMeshPro>();
        displayNumber.text = generatedNumber.ToString();
    }
    public int GetGeneratedNumber() { return generatedNumber; }

    public void SetCable()
    {
        GetComponentInChildren<Cable>().SetCable();
    }
}
