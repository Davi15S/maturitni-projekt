using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TilemapScript : MonoBehaviour
{
    [SerializeField] bool isRobotics = false;
    [SerializeField] Transform testObject;
    [SerializeField] Transform parent;
    private List<Vector3> blockedPos = new List<Vector3>();
    private List<Vector3> logicGateBlockedPos = new List<Vector3>();
    private Tilemap tilemap;


    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        if (isRobotics && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildLogicGate();
        }
    }

    private void BuildLogicGate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 cellWorldPos = GetCellWordlPosition();

            if (!blockedPos.Contains(cellWorldPos) && !logicGateBlockedPos.Contains(cellWorldPos) && CanBuild(cellWorldPos))
            {
                Instantiate(testObject, cellWorldPos, Quaternion.identity, parent);

                Vector3Int cellPos = tilemap.WorldToCell(cellWorldPos);
                CycleLogicGatePositions(cellPos, false);
            }
            else
            {
                Debug.Log("Cant build there");
            }
        }
    }

    private bool CanBuild(Vector3 pos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(pos);

        for (int x = cellPos.x - 1; x < cellPos.x + 2; x++)
        {
            for (int y = cellPos.y - 1; y < cellPos.y + 2; y++)
            {
                if (blockedPos.Contains(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0))) || logicGateBlockedPos.Contains(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0))) || !tilemap.GetTile(new Vector3Int(x, y, 0)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void CycleLogicGatePositions(Vector3Int centerPos, bool isDelete)
    {
        for (int x = centerPos.x - 1; x < centerPos.x + 2; x++)
        {
            for (int y = centerPos.y - 1; y < centerPos.y + 2; y++)
            {
                if (isDelete)
                {
                    RemoveFromList(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)));
                }
                else
                {
                    AddToList(tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0)));
                }
            }
        }
    }

    public void RemoveLogicGateFromList(Vector3 pos, Vector3 ouputPos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(pos);
        CycleLogicGatePositions(cellPos, true);
        RemoveFromLogicGateList(ouputPos);
    }

    public void AddToList(Vector3 pos)
    {
        blockedPos.Add(pos);
    }
    public void RemoveFromList(Vector3 pos)
    {
        blockedPos.RemoveAll(x => x == pos);
    }
    public void ClearAllLists()
    {
        blockedPos.Clear();
        logicGateBlockedPos.Clear();
    }
    public List<Vector3> GetList()
    {
        return blockedPos;
    }

    public void AddToLogicGateList(Vector3 pos)
    {
        logicGateBlockedPos.Add(pos);
    }

    public void RemoveRangeLogicGateList(int index)
    {
        logicGateBlockedPos.RemoveRange(index + 1, (logicGateBlockedPos.Count - index - 1));
    }

    public void RemoveFromLogicGateList(Vector3 pos)
    {
        logicGateBlockedPos.Remove(pos);
    }

    public List<Vector3> GetLogicGateBlockList()
    {
        return logicGateBlockedPos;
    }

    public Vector3 GetMouseWorldPosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var noZ = new Vector3(pos.x, pos.y);
        return noZ;
    }
    public Vector3 GetCellWordlPosition()
    {
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(GetMouseCellPosition());
        return cellWorldPos;
    }
    public Vector3Int GetMouseCellPosition()
    {
        return tilemap.WorldToCell(GetMouseWorldPosition());
    }
}