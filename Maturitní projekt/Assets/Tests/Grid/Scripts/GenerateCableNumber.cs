using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateCableNumber : MonoBehaviour
{
    private TextMeshPro displayNumber;
    void Start()
    {
        displayNumber = GetComponentInChildren<TextMeshPro>();
        displayNumber.text = GenerateNumber().ToString();
    }

    void Update()
    {

    }

    private int GenerateNumber()
    {
        return Random.Range(0, 11);
    }
}
