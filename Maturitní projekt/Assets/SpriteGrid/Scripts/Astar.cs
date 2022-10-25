using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

public enum TileType { START, GOAL, WATER, GRASS, PATH }

public class Astar : MonoBehaviour
{
    private TileType tileType;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Tile[] tiles;
    private Vector3Int startPos, goalPos;
    private HashSet<Node> openList;
    private HashSet<Node> closedList;
    private Stack<Vector3Int> path;
    private List<Vector3Int> blockedTiles = new List<Vector3Int>();
    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();
    private Node current;
    private int gScore = 10;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

            // if (hit.collider != null)
            // {
            //     Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //     Vector3Int clickPos = tilemap.WorldToCell(mouseWorldPos);

            //     ChangeTile(clickPos);
            // }
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var noZ = new Vector3(pos.x, pos.y);
            Vector3Int mouseCell = tilemap.WorldToCell(noZ);
            var tileUnderMouse = tilemap.GetTile(mouseCell);

            if (tileUnderMouse == tiles[0])
            {
                startPos = mouseCell;
            }
            else if (tileUnderMouse == tiles[1])
            {
                goalPos = mouseCell;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Run algorithm
            Algorithm();
        }
    }

    private void Initialize()
    {
        Debug.Log("Initializace");
        current = GetNode(startPos);
        openList = new HashSet<Node>();
        closedList = new HashSet<Node>();
        openList.Add(current);
    }

    private void Algorithm()
    {
        if (current == null) { Initialize(); }
        while (openList.Count > 0 && path == null)
        {
            List<Node> neighbors = FindNeighbors(current.Position);
            ExamineNeighbors(neighbors, current);
            UpdateCurrentTile(ref current);
            path = GeneratePath(current);
        }

        if (path != null)
        {
            foreach (Vector3Int pos in path)
            {
                if (pos != startPos && pos != goalPos)
                {
                    tilemap.SetTile(pos, tiles[4]);
                }
            }
        }
        openList.Clear();
        closedList.Clear();
        current = null;
        // AstarDebugger.MyInstance.CreateTiles(openList, closedList, allNodes, startPos, goalPos, path);
    }

    private List<Node> FindNeighbors(Vector3Int parentPos)
    {
        List<Node> neighbors = new List<Node>();
        blockedTiles = GenerateLevel.MyInstance.BlockedTiles();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int neighborPos = new Vector3Int(parentPos.x - x, parentPos.y - y, parentPos.z);

                if ((x == 0 || y == 0) && neighborPos != startPos && tilemap.GetTile(neighborPos) && !blockedTiles.Contains(neighborPos))
                {
                    Node neighbor = GetNode(neighborPos);
                    neighbors.Add(neighbor);
                }
            }
        }
        return neighbors;
    }

    private void ExamineNeighbors(List<Node> neighbors, Node current)
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            Node neighbor = neighbors[i];

            if (openList.Contains(neighbor))
            {
                if (current.G + gScore < neighbor.G)
                {
                    CalcValues(current, neighbor, gScore);
                }
            }
            else if (!closedList.Contains(neighbor))
            {
                CalcValues(current, neighbor, gScore);
                openList.Add(neighbor);
            }
        }
    }

    private void CalcValues(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;
        neighbor.G = parent.G + cost;
        neighbor.H = ((Math.Abs(neighbor.Position.x - goalPos.x) + Math.Abs(neighbor.Position.y - goalPos.y)) * 10);
        neighbor.F = neighbor.G + neighbor.H;
    }

    private void UpdateCurrentTile(ref Node current)
    {
        openList.Remove(current);
        closedList.Add(current);

        if (openList.Count > 0) { current = openList.OrderBy(x => x.F).First(); }
    }

    private Node GetNode(Vector3Int pos)
    {
        if (!allNodes.ContainsKey(pos))
        {
            Node node = new Node(pos);
            allNodes.Add(pos, node);
        }

        return allNodes[pos];
    }

    public void ChangeTileType(TileButton button)
    {
        tileType = button.MyTileType;
    }

    private void ChangeTile(Vector3Int clickPos)
    {
        if (tileType == TileType.START) { startPos = clickPos; }
        else if (tileType == TileType.GOAL) { goalPos = clickPos; }
        else if (tileType == TileType.WATER)
        {
            tilemap.SetTile(clickPos, tiles[(int)tileType]);
        }
        tilemap.SetTile(clickPos, tiles[(int)tileType]);
    }

    private Stack<Vector3Int> GeneratePath(Node current)
    {
        if (current.Position == goalPos)
        {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();
            while (current.Position != startPos)
            {
                finalPath.Push(current.Position);
                current = current.Parent;
            }
            return finalPath;
        }
        return null;
    }
}
