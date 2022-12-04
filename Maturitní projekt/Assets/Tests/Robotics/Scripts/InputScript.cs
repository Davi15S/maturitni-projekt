using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputScript : MonoBehaviour
{
    private Tilemap tilemap;
    private TilemapScript tilemapScript;

    void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        tilemapScript = GetComponentInParent<TilemapScript>();
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
        tilemapScript.RemoveFromList(transform.position);
    }
    void Update()
    {

    }
}
