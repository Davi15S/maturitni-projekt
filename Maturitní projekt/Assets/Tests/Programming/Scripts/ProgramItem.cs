using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProgramItem", menuName = "ProgramItem")]
public class ProgramItem : ScriptableObject
{
    public string word;
    public List<string> replaceWords;
}
