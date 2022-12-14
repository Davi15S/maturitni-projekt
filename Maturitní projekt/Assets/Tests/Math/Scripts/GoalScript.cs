using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using TMPro;

public class GoalScript : MonoBehaviour
{
    private MathOperations mathOperation;

    [SerializeField] private Vector3Int startCellPosition;
    [SerializeField] private Tilemap tilemap;

    private SpriteRenderer sprite;
    private TextMeshPro displayText;

    private bool[] dataSent = { false, true };
    private float calculatedNumber;
    private int generatedNumber;
    private int cableGeneratedNumber;
    private string defaultText;

    void Start()
    {
        displayText = GetComponentInChildren<TextMeshPro>();
        sprite = GetComponent<SpriteRenderer>();

        transform.position = tilemap.GetCellCenterWorld(startCellPosition);
        mathOperation = (MathOperations)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MathOperations)).Length);

        GenerateNumber();

        defaultText = mathOperation.ToString() + " " + generatedNumber;
        displayText.text = defaultText;
    }
    void Update()
    {
        CheckCollider();
    }

    // Kontrola, jestli nějaký objekt necollidnul s cílem
    void CheckCollider()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.tag == "Cable")
            {
                sprite.color = Color.red;
                cableGeneratedNumber = collider.GetComponent<Cable>().GetGeneratedNumber();

                // Debug text
                displayText.text = cableGeneratedNumber + " " + defaultText;

                if (!dataSent[0])
                {
                    GeneratedNumberManager.instance.CalculateTotalNumber(CalculateNumber(), false);
                }
            }

            // Objekt necolliduje s kabelem
            else if (!Array.Exists(colliders, x => x.tag == "Cable"))
            {
                // Debug text
                displayText.text = defaultText;

                sprite.color = Color.yellow;
                if (!dataSent[1]) { CableUnConnect(); }
            }
        }
    }

    private void GenerateNumber() { generatedNumber = UnityEngine.Random.Range(1, 11); }
    private float CalculateNumber()
    {
        dataSent[0] = true;
        dataSent[1] = false;
        calculatedNumber = GeneratedNumberManager.instance.CalculateNumber(cableGeneratedNumber, generatedNumber, mathOperation);
        return calculatedNumber;
    }
    private void CableUnConnect()
    {
        dataSent[1] = true;
        dataSent[0] = false;
        GeneratedNumberManager.instance.CalculateTotalNumber(calculatedNumber, true);
    }
    public int GetGeneratedNumber() { return generatedNumber; }
    public MathOperations GetMathOperation() { return mathOperation; }
}
