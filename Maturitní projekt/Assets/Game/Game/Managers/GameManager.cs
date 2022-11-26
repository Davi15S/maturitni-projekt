using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    private int level;
    public static GameManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    public void LoadData(GameData data) { this.level = data.level; }
    public void SaveData(ref GameData data) { data.level = level; }
    public int GetLevel() { return level; }
}
