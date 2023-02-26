using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEManager : MonoBehaviour, IDataPersistence
{
    public static PEManager instance { get; private set; }

    [SerializeField] private GameObject gameOverPrefab;
    private float gameSpeed;
    private float initialGameSpeed;
    [SerializeField] private float gameSpeedIncrease = 1.05f;
    private float maxGameSpeed;
    private GameData.Level[] levels;
    private int level;

    public void LoadData(GameData data)
    {
        this.levels = data.levels;
        this.level = data.level;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        gameSpeed = initialGameSpeed;
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPrefab.SetActive(true);
    }

    public void GameWon()
    {
        Debug.Log("Game Won");
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.PE);
    }
}
