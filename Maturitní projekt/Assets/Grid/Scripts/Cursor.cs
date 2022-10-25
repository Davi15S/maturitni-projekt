using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    private static Cursor instance;
    public static Cursor MyInstance { get { if (instance == null) { instance = FindObjectOfType<Cursor>(); } return instance; } }
    private bool isDragged = false;
    private Vector3Int startCellPosition = new Vector3Int(-6, 1);
    [SerializeField] private Tilemap tilemap;
    private List<Vector3> cursorPositionList = new List<Vector3>();
    private Sprite mySprite;

    private void Start()
    {
        transform.position = tilemap.GetCellCenterWorld(startCellPosition);
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var noZ = new Vector3(pos.x, pos.y);
        Vector3Int mouseCell = tilemap.WorldToCell(noZ);
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(mouseCell);

        if (isDragged && tilemap.GetTile(mouseCell))
        {
            transform.position = cellWorldPos;
            if (!cursorPositionList.Contains(cellWorldPos))
            {
                cursorPositionList.Add(cellWorldPos);
                Debug.Log(cursorPositionList);
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            isDragged = true;
        }
        else { isDragged = false; }
    }

    public List<Vector3> getPointsList()
    {
        return cursorPositionList;
    }
}
