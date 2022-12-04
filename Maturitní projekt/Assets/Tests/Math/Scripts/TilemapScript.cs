using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    [SerializeField] bool isRobotics = false;
    [SerializeField] Transform testObject;
    private List<Vector3> blockedPos = new List<Vector3>();
    private List<Vector3> logicGateBlockedPos = new List<Vector3>();
    private Tilemap tilemap;


    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        if (isRobotics)
        {
            BuildLogicGate();
        }
    }

    private void BuildLogicGate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 cellWorldPos = GetCellWordlPosition();

            if (!blockedPos.Contains(cellWorldPos) && !logicGateBlockedPos.Contains(cellWorldPos))
            {
                Instantiate(testObject, cellWorldPos, Quaternion.identity);
                AddToList(cellWorldPos);
            }
            else
            {
                Debug.Log("Cant build there");
            }
        }
    }

    public void AddToList(Vector3 pos)
    {
        blockedPos.Add(pos);
    }
    public void RemoveFromList(Vector3 pos)
    {
        blockedPos.RemoveAll(x => x == pos);
    }
    public List<Vector3> GetList()
    {
        return blockedPos;
    }

    public void SetLogicGateBlockedList(List<Vector3> pos)
    {
        // logicGateBlockedPos.Clear();
        // logicGateBlockedPos.AddRange(pos);
        // foreach (var item in logicGateBlockedPos)
        // {
        //     Debug.Log(item);
        // }
    }

    public void AddToLogicGateList(Vector3 pos)
    {
        logicGateBlockedPos.Add(pos);
    }

    public void RemoveRangeLogicGateList(int index)
    {
        logicGateBlockedPos.RemoveRange(index, (logicGateBlockedPos.Count - index));
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
