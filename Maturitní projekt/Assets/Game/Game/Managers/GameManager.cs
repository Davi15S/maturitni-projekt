using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public void LoadData(GameData data)
    {
        this.level = data.level;
        CheckFinishedQuizes(data);
    }
    public void SaveData(ref GameData data) { data.level = level; }
    public int GetLevel() { return level; }

    private void CheckFinishedQuizes(GameData data)
    {
        foreach (GameData.Level gameLevel in data.levels)
        {
            if (gameLevel.subjects.All(x => x.finished == true) && gameLevel.level == data.level)
            {
                Debug.Log("Finished level!");
                level++;
                Debug.Log("Current level: " + level);
                DataPersistenceManager.instance.SaveGame();
                DataPersistenceManager.instance.LoadGame();
            }
        }
    }
}
