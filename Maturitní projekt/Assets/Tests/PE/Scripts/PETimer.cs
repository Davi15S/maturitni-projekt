using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PETimer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float gameTime;
    [SerializeField] private GameObject gameCanvas;
    private float timeCache;

    void OnEnable()
    {
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
            Time.timeScale = 0f;
            gameCanvas.SetActive(true);
            PEManager.instance.GameWon();
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
}