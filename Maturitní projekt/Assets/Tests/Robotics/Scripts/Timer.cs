using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float gameTime;
    [SerializeField] private GameObject gameCanvas;

    void Awake()
    {
        slider.maxValue = gameTime;
        slider.value = gameTime;
    }

    void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            Time.timeScale = 0f;
            gameCanvas.SetActive(true);
        }
        else
        {
            slider.value = gameTime;
        }
    }
}
