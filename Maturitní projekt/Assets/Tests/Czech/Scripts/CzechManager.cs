using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string question;
    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    public string answer;
    public bool result;
}

[System.Serializable]
public class QuestionsList
{
    public Question[] questions;
}

public class CzechManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextAsset dataJson;
    [SerializeField] private TextMeshProUGUI questionCanvas;
    [SerializeField] private GameObject buttonsGameObject;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Timer timer;
    private Button[] buttons;
    private QuestionsList questionsList = new QuestionsList();
    public static CzechManager instance { get; private set; }
    private int question = 0;

    private GameData.Level[] levels;
    private int level;

    public void LoadData(GameData data)
    {
        this.level = data.level;
        this.levels = data.levels;
    }
    public void SaveData(ref GameData data)
    {
        data.levels = this.levels;
    }

    void Start()
    {
        if (instance != null) { Destroy(this.gameObject); }
        else { instance = this; }

        buttons = buttonsGameObject.GetComponentsInChildren<Button>();
        questionsList = JsonUtility.FromJson<QuestionsList>(dataJson.text);

        Shuffle();
        SetQuestion();
    }

    private void Shuffle()
    {
        for (int i = 0; i < questionsList.questions.Length; i++)
        {
            int rnd = Random.Range(0, questionsList.questions.Length);
            Question tempGO = questionsList.questions[rnd];
            questionsList.questions[rnd] = questionsList.questions[i];
            questionsList.questions[i] = tempGO;
        }
    }

    private void SetAnswers()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = questionsList.questions[question].answers[i].answer;
            buttons[i].GetComponent<ButtonScript>().SetIndex(i);
            buttons[i].GetComponent<Image>().color = Color.white;
        }
    }

    private void SetQuestion()
    {
        questionCanvas.text = questionsList.questions[question].question;
        SetAnswers();
        timer.gameObject.SetActive(true);
    }

    public void CheckAnswer(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (questionsList.questions[question].answers[i].result)
            {
                buttons[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = Color.red;
            }
        }

        if (question >= 3)
        {
            GameWon();
        }
        else
        {
            if (questionsList.questions[question].answers[index].result)
            {
                Debug.Log("Odpověď byla správná!");
                timer.gameObject.SetActive(false);
                FunctionTimer.Create(SetQuestion, 3f);
                question++;
            }
            else
            {
                Debug.Log("Odpověď nebyla správná!");
                timer.gameObject.SetActive(false);
                FunctionTimer.Create(timer.GameOver, 3f);
            }
        }
    }

    private void GameWon()
    {
        Time.timeScale = 0f;
        DataPersistenceManager.instance.FinishQuiz(levels, level, Subject.Czech);
    }
}
