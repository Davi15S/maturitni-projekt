using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC")]
public class NPCObject : ScriptableObject
{
    public Sprite sprite;
    public string name;
    public Subject subject;
    public RuntimeAnimatorController controller;
}
