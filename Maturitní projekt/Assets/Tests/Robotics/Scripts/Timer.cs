using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float gameTime;
    private float timeCache;
    [SerializeField] private GameOver gameOver;

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        SetTimer();
    }

    void Awake()
    {
        SetTimer();
    }

    void Update()
    {
        timeCache -= Time.deltaTime;

        if (timeCache <= 0)
        {
            GameOver();
        }
        else
        {
            slider.value = timeCache;
        }
    }

    private void SetTimer()
    {
        slider.maxValue = gameTime;
        slider.value = gameTime;
        timeCache = gameTime;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.gameObject.SetActive(true);
    }
}
