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

    public void CreateTiles(HashSet<Node> openList, Vector3Int start, Vector3Int goal)
    {
        foreach (Node node in openList)
        {
            ColorTile(node.Position, openColor);
        }

        ColorTile(start, startColor);
        ColorTile(goal, goalColor);
    }

    public void ColorTile(Vector3Int pos, Color color)
    {
        tilemap.SetTile(pos, tile);
        tilemap.SetTileFlags(pos, TileFlags.None);
        tilemap.SetColor(pos, color);
    }
}
