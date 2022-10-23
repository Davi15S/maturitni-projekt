using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();
    private Node current;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPos = tilemap.WorldToCell(mouseWorldPos);

                ChangeTile(clickPos);
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
        current = GetNode(startPos);
        openList = new HashSet<Node>();
        openList.Add(current);
    }

    private void Algorithm()
    {
        if (current == null) { Initialize(); }
        List<Node> neighbors = FindNeighbors(current.Position);
        ExamineNeighbors(neighbors, current);
        AstarDebugger.MyInstance.CreateTiles(openList, startPos, goalPos);
    }

    private List<Node> FindNeighbors(Vector3Int parentPos)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int neighborPos = new Vector3Int(parentPos.x - x, parentPos.y - y, parentPos.z);

                if ((x == 0 || y == 0) && neighborPos != startPos && tilemap.GetTile(neighborPos))
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
            openList.Add(neighbors[i]);
        }
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
        tilemap.SetTile(clickPos, tiles[(int)tileType]);
    }
}
