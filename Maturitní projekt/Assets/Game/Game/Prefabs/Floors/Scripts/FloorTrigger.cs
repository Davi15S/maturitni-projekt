using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private bool isNextFloor;
    [SerializeField] private Transform portPos;
    private float portOffset = 0.8f;
    void OnTriggerExit2D(Collider2D other)
    {
        FloorManager.instance.LoadFloor(isNextFloor ? true : false);
        other.transform.position = new Vector3(portPos.position.x, portPos.position.y + portOffset, 0);
    }
}
