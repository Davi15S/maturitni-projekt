using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class InputScript : MonoBehaviour
{
    private Tilemap tilemap;
    private TilemapScript tilemapScript;
    private bool isPositive = false;
    private SpriteRenderer sprite;

    void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        tilemapScript = GetComponentInParent<TilemapScript>();
        transform.position = tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position));
        tilemapScript.RemoveFromList(transform.position);
        sprite = GetComponent<SpriteRenderer>();
        CheckConnection();
    }
    void Update()
    {
        CheckColliders();
        CheckConnection();
    }

    private void CheckColliders()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0f);
        if (colliders.Length < 1)
        {
            isPositive = false;
        }
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.tag == "Cable" && collider.GetComponent<Cable>().GetConnection())
            {
                isPositive = true;
            }
            else if (!Array.Exists(colliders, x => x.tag == "Cable"))
            {
                isPositive = false;
            }
        }
    }

    private void CheckConnection()
    {
        if (isPositive)
        {
            sprite.color = Color.green;
        }
        else
        {
            sprite.color = Color.red;
        }
    }

    public bool GetConnection()
    {
        return isPositive;
    }
}
