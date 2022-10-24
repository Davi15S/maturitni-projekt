using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AstarDebugger : MonoBehaviour
{
    private static AstarDebugger instance;
    public static AstarDebugger MyInstance
    {
        get { if (instance == null) { instance = FindObjectOfType<AstarDebugger>(); } return instance; }
    }

    [SerializeField] private Grid grid;
    [SerializeField] private Tile tile;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Color openColor, closedColor, pathColor, currenColor, startColor, goalColor;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject debugTextPref;
    private List<GameObject> debugObjects = new List<GameObject>();

    public void CreateTiles(HashSet<Node> openList, HashSet<Node> closedList, Dictionary<Vector3Int, Node> allNodes, Vector3Int start, Vector3Int goal, Stack<Vector3Int> path = null)
    {
        foreach (Node node in openList) { ColorTile(node.Position, openColor); }
        foreach (Node node in closedList) { ColorTile(node.Position, closedColor); }

        if (path != null)
        {
            foreach (Vector3Int pos in path)
            {
                if (pos != start && pos != goal)
                {
                    ColorTile(pos, pathColor);
                }
            }
        }

        ColorTile(start, startColor);
        ColorTile(goal, goalColor);

        foreach (KeyValuePair<Vector3Int, Node> node in allNodes)
        {
            if (node.Value.Parent != null)
            {
                GameObject go = Instantiate(debugTextPref, canvas.transform);
                go.transform.position = grid.CellToWorld(node.Key);
                debugObjects.Add(go);
                GenerateDebugText(node.Value, go.GetComponent<DebugText>());
            }
        }
    }

    private void GenerateDebugText(Node node, DebugText debugText)
    {
        debugText.P.text = $"P:{node.Position.x}, {node.Position.y}";
        debugText.F.text = $"F:{node.F}";
        debugText.G.text = $"G:{node.G}";
        debugText.H.text = $"H:{node.H}";
        if (node.Parent.Position.x < node.Position.x && node.Position.y == node.Parent.Position.y) { debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 180)); }
        else if (node.Parent.Position.x == node.Position.x && node.Position.y < node.Parent.Position.y) { debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 90)); }
        else if (node.Parent.Position.x == node.Position.x && node.Position.y > node.Parent.Position.y) { debugText.MyArrow.localRotation = Quaternion.Euler(new Vector3(0, 0, -90)); }
    }

    public void ColorTile(Vector3Int pos, Color color)
    {
        tilemap.SetTile(pos, tile);
        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
    }
}
