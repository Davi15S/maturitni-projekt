using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private bool isNextFloor;
    void OnTriggerExit2D() { FloorManager.instance.LoadFloor(isNextFloor ? true : false); }
}
