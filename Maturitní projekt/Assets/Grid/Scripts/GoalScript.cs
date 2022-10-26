using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class GoalScript : MonoBehaviour
{
    [SerializeField] private Vector3Int startCellPosition;
    [SerializeField] private Tilemap tilemap;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        transform.position = tilemap.GetCellCenterWorld(startCellPosition);
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.tag == "Cable") { sprite.color = Color.red; }
            else if (!Array.Exists(colliders, x => x.tag == "Cable")) { sprite.color = Color.yellow; }
        }
    }
}
