using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour, IDataPersistence
{
    private static int currentFloor = 0;
    [SerializeField] private List<GameObject> floors = new List<GameObject>();
    public static FloorManager instance { get; private set; }

    public void LoadData(GameData data) { currentFloor = data.floor; }
    public void SaveData(ref GameData data) { data.floor = currentFloor; }

    private void Start()
    {
        if (instance != null) { Destroy(this.gameObject); }
        else { instance = this; }
        foreach (GameObject floor in floors) { floor.SetActive(false); }
        floors[currentFloor].SetActive(true);
    }

    public void LoadFloor(bool next)
    {
        floors[currentFloor].SetActive(false);
        if (next) { currentFloor++; }
        else { currentFloor--; }
        floors[currentFloor].SetActive(true);
    }

    public int GetCurrentFloor() { return currentFloor; }
}
