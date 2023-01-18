using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private NpcScript npcScript;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        npcScript = GetComponentInParent<NpcScript>();

        spriteRenderer.sprite = npcScript.npc.sprite;
        animator.runtimeAnimatorController = npcScript.npc.controller;
    }
}
