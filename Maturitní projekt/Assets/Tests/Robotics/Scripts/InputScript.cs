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
    [SerializeField] LayerMask mask;
    [SerializeField] Sprite spriteConnected;
    [SerializeField] Sprite spriteUnconnected;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0f, mask);
        if (colliders.Length <= 0)
        {
            isPositive = false;
        }
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.tag == "Cable" && collider.GetComponent<Cable>().GetConnection())
            {
                isPositive = true;
            }
            else
            {
                isPositive = false;
            }
        }
    }

    private void CheckConnection()
    {
        if (isPositive)
        {
            sprite.sprite = spriteConnected;
        }
        else
        {
            sprite.sprite = spriteUnconnected;
        }
    }

    public bool GetConnection()
    {
        return isPositive;
    }
}
