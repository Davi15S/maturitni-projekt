using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorHandle : MonoBehaviour
{
    private static int currentFloor = 0;
    [SerializeField] private List<GameObject> floors = new List<GameObject>();

    public void LoadFloor(bool next)
    {
        floors[currentFloor].SetActive(false);
        if (next) { currentFloor++; }
        else { currentFloor--; }
        floors[currentFloor].SetActive(true);
    }
}
