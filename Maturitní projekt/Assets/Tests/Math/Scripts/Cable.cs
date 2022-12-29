using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Cable : MonoBehaviour
{
    [SerializeField] private Vector3Int startCellPosition;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private bool isRobotics;
    [SerializeField] private bool isPositive;

    private bool isDragged = false;
    private List<Vector3> cursorPositionList = new List<Vector3>();
    private SpriteRenderer mySprite;
    private TilemapScript tileMapScript;
    private int generatedNumber;

    private LogicGateType type;

    [SerializeField] private bool isDebug;


    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        tileMapScript = GetComponentInParent<TilemapScript>();
        tilemap = GetComponentInParent<Tilemap>();
        // Testing committing to main branch : AGAIN 3x

        if (!isRobotics)
        {
            transform.position = tilemap.GetCellCenterWorld(startCellPosition);
            cursorPositionList.Add(tilemap.GetCellCenterWorld(startCellPosition));
        }
        else
        {
            transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
            cursorPositionList.Add(transform.position);
            tileMapScript.AddToLogicGateList(transform.position);
            CheckConnection();
        }
    }

    private void FixedUpdate()
    {
        DragCable();
        CheckConnection();
    }

    void DragCable()
    {
        Vector3Int mouseCell = tileMapScript.GetMouseCellPosition();
        Vector3 cellWorldPos = tileMapScript.GetCellWordlPosition();

        // Zkontrolovat, zda místo, kam chci táhnout kabel, je přístupný
        if (isDragged && tilemap.GetTile(mouseCell) && IsNeighbor(cellWorldPos) && !tileMapScript.GetList().Contains(cellWorldPos))
        {
            transform.position = cellWorldPos;
            tileMapScript.RemoveFromList(cursorPositionList.Last());
            tileMapScript.AddToList(cellWorldPos);

            if (!cursorPositionList.Contains(cellWorldPos))
            {
                cursorPositionList.Add(cellWorldPos);
                tileMapScript.AddToLogicGateList(cellWorldPos);
            }
            else
            {
                int index = cursorPositionList.FindIndex(x => x == cellWorldPos);
                cursorPositionList.RemoveRange(index + 1, (cursorPositionList.Count - index - 1));
                tileMapScript.RemoveRangeLogicGateList(index);
            }
        }
        else if (tilemap.GetTile(mouseCell) || tileMapScript.GetList().Contains(cellWorldPos)) { isDragged = false; }
    }

    private bool IsNeighbor(Vector3 pos)
    {
        if ((pos.x == cursorPositionList.Last().x && pos.y > cursorPositionList.Last().y) || (pos.x == cursorPositionList.Last().x && pos.y < cursorPositionList.Last().y) || (pos.x > cursorPositionList.Last().x && pos.y == cursorPositionList.Last().y) || (pos.x < cursorPositionList.Last().x && pos.y == cursorPositionList.Last().y)) { return true; }
        else { return false; }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) { isDragged = true; }
        else { isDragged = false; }
    }

    public List<Vector3> GetPointsList() { return cursorPositionList; }
    public int GetGeneratedNumber() { return generatedNumber; }
    public void SetGeneratedNumber(int number) { generatedNumber = number; }
    private void CheckConnection()
    {
        if (isPositive)
        {
            mySprite.color = Color.green;
        }
        else
        {
            mySprite.color = Color.red;
        }
    }
    public void SetConnection(bool connection, LogicGateType logicType)
    {
        type = logicType;
        isPositive = connection;
    }
    public bool GetConnection()
    {
        return isPositive;
    }

    public LogicGateType GetType()
    {
        return type;
    }
}
