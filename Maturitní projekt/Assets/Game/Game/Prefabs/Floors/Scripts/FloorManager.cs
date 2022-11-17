using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private bool isNextFloor;
    void OnTriggerExit2D(Collider2D player)
    {
        player.GetComponent<FloorHandle>().LoadFloor(isNextFloor ? true : false);
    }
}
