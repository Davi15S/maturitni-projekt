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
    [SerializeField] private Sprite spriteConnected;
    [SerializeField] private Sprite spriteUnconnected;

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
                RotateCable(cellWorldPos, cursorPositionList.Last());
                cursorPositionList.Add(cellWorldPos);
                tileMapScript.AddToLogicGateList(cellWorldPos);
            }
            else
            {
                int index = cursorPositionList.FindIndex(x => x == cellWorldPos);
                cursorPositionList.RemoveRange(index + 1, (cursorPositionList.Count - index - 1));
                tileMapScript.RemoveRangeLogicGateList(index);
                RotateCable(cursorPositionList.Last(), cursorPositionList[cursorPositionList.Count - 2]);
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
        mySprite.sprite = connection ? spriteConnected : spriteUnconnected;
    }
    public bool GetConnection()
    {
        return isPositive;
    }

    private void RotateCable(Vector3 currentPosition, Vector3 lastPosition)
    {
        Debug.Log(currentPosition - lastPosition);

        if ((currentPosition.x - lastPosition.x) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if ((currentPosition.x - lastPosition.x) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if ((currentPosition.y - lastPosition.y) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((currentPosition.y - lastPosition.y) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
