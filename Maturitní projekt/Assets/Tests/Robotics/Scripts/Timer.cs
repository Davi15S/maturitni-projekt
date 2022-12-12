using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float gameTime;
    [SerializeField] private GameObject lostGameCanvas;
    void Start()
    {
        slider.maxValue = gameTime;
        slider.value = gameTime;
    }

    void Update()
    {
        float time = gameTime - Time.time;

        if (time <= 0)
        {
            Time.timeScale = 0f;
            lostGameCanvas.SetActive(true);
        }
        else
        {
            slider.value = time;
        }
    }

    public void GoBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
