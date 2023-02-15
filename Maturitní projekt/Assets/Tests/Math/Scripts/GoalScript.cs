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
    [SerializeField] private Sprite addSprite;
    [SerializeField] private Sprite substractSprite;
    [SerializeField] private Sprite divideSprite;
    [SerializeField] private Sprite multiplySprite;

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
        SetGoal();
        SetSprite();
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

                if (!dataSent[1]) { CableUnConnect(); }
            }
        }
    }

    public void GenerateNumber()
    {
        generatedNumber = UnityEngine.Random.Range(1, 11);
        defaultText = mathOperation.ToString() + " " + generatedNumber;
        displayText.text = defaultText;
    }
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
    public void SetGoal()
    {
        mathOperation = (MathOperations)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MathOperations)).Length);
    }
    private void SetSprite()
    {
        switch (mathOperation)
        {
            case MathOperations.ADD:
                sprite.sprite = addSprite;
                break;
            case MathOperations.SUBSTRACT:
                sprite.sprite = substractSprite;
                break;
            case MathOperations.MULTIPLY:
                sprite.sprite = multiplySprite;
                break;
            case MathOperations.DIVIDE:
                sprite.sprite = divideSprite;
                break;
            default:
                break;
        }
    }
}