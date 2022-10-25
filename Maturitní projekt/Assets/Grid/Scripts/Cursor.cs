using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Cursor : MonoBehaviour
{
    private bool isDragged = false;
    [SerializeField] private Vector3Int startCellPosition;
    [SerializeField] private Tilemap tilemap;
    private List<Vector3> cursorPositionList = new List<Vector3>();
    private SpriteRenderer mySprite;

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        transform.position = tilemap.GetCellCenterWorld(startCellPosition);
        cursorPositionList.Add(tilemap.GetCellCenterWorld(startCellPosition));
    }

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var noZ = new Vector3(pos.x, pos.y);
        Vector3Int mouseCell = tilemap.WorldToCell(noZ);
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(mouseCell);

        if (!tilemap.GetTile(mouseCell) && !isNeighbor(cellWorldPos))
        {
            isDragged = false;
        }

        if (isDragged && tilemap.GetTile(mouseCell) && isNeighbor(cellWorldPos))
        {
            transform.position = cellWorldPos;
            if (!cursorPositionList.Contains(cellWorldPos))
            {
                cursorPositionList.Add(cellWorldPos);
            }
            else if (cursorPositionList.Last() != cellWorldPos)
            {
                int index = cursorPositionList.FindIndex(x => x == cellWorldPos);
                cursorPositionList.RemoveRange(index + 1, (cursorPositionList.Count - index - 1));
            }
        }
    }

    private bool isNeighbor(Vector3 pos)
    {
        if (cursorPositionList.Count > 1)
        {
            if ((pos.x == cursorPositionList.Last().x && pos.y > cursorPositionList.Last().y) || (pos.x == cursorPositionList.Last().x && pos.y < cursorPositionList.Last().y) || (pos.x > cursorPositionList.Last().x && pos.y == cursorPositionList.Last().y) || (pos.x < cursorPositionList.Last().x && pos.y == cursorPositionList.Last().y))
            {
                return true;
            }
            else { return false; }
        }
        else { return true; }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) { isDragged = true; mySprite.color = Color.red; }
        else { isDragged = false; }
    }

    void OnMouseExit()
    {
        mySprite.color = Color.white;
    }

    public List<Vector3> getPointsList()
    {
        return cursorPositionList;
    }
}
