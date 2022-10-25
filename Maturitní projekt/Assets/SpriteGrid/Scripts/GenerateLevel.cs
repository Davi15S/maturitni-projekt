using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] tiles;
    private List<Vector3Int> blockedTiles = new List<Vector3Int>();

    private static GenerateLevel instance;
    public static GenerateLevel MyInstance
    {
        get { if (instance == null) { instance = FindObjectOfType<GenerateLevel>(); } return instance; }
    }

    public List<Vector3Int> BlockedTiles()
    {
        return blockedTiles;
    }

    private void Start()
    {
        GenerateLevel1();
    }

    private void GenerateLevel1()
    {
        tilemap.SetTile(new Vector3Int(-6, 1, 0), tiles[0]);
        blockedTiles.Add(new Vector3Int(-6, 1, 0));

        tilemap.SetTile(new Vector3Int(5, 1, 0), tiles[1]);

        tilemap.SetTile(new Vector3Int(-6, -1, 0), tiles[0]);

        blockedTiles.Add(new Vector3Int(-6, -1, 0));
        tilemap.SetTile(new Vector3Int(5, -1, 0), tiles[1]);

        tilemap.SetTile(new Vector3Int(-6, -3, 0), tiles[0]);
        blockedTiles.Add(new Vector3Int(-6, -3, 0));

        tilemap.SetTile(new Vector3Int(5, -3, 0), tiles[1]);
    }
}
