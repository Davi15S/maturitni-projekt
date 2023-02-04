using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeIfNull = false;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene) { SaveGame(); }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null && initializeIfNull) { NewGame(); }
        if (this.gameData == null) { return; }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) { dataPersistenceObj.LoadData(gameData); }
    }
    public void SaveGame()
    {
        if (this.gameData == null) { return; }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) { dataPersistenceObj.SaveData(ref gameData); }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() { SaveGame(); }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData() { return gameData != null; }

    public void FinishQuiz(GameData.Level[] levels, int level, Subject subject)
    {
        foreach (GameData.Level gameLevel in levels)
        {
            if (level == gameLevel.level)
            {
                foreach (GameData.LevelSubject levelSubject in gameLevel.subjects)
                {
                    if (levelSubject.subject == subject)
                    {
                        Debug.Log($"Level: {level} | Subject: {levelSubject.subject.ToString()}");
                        Debug.Log($"Progress:  {levelSubject.finished}");
                        levelSubject.finished = true;
                    }
                }
            }
        }

        DataPersistenceManager.instance.SaveGame();
    }
}
