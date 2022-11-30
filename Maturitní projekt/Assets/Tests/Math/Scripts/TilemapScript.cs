using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScript : MonoBehaviour
{
    private List<Vector3> blockedPos = new List<Vector3>();
    public void AddToList(Vector3 pos) { blockedPos.Add(pos); }
    public void RemoveFromList(Vector3 pos) { blockedPos.RemoveAll(x => x == pos); }
    public List<Vector3> GetList() { return blockedPos; }
}
