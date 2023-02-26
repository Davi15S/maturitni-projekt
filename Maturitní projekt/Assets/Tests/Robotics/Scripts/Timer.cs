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
    private bool isStopped;
    private float timeCache;
    private Animator animator;

    void OnEnable()
    {
        SetTimer();
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        SetTimer();
    }

    void Update()
    {
        if (!isStopped)
        {
            timeCache -= Time.deltaTime;

            if (timeCache <= 0)
            {
                Time.timeScale = 0f;
                gameCanvas.SetActive(true);
            }
            else
            {
                slider.value = timeCache;
            }
        }
    }

    public void SetTimer()
    {
        slider.maxValue = gameTime;
        slider.value = gameTime;
        timeCache = gameTime;
        animator.enabled = true;
        isStopped = false;
    }

    public void StopTimer()
    {
        isStopped = true;
        animator.enabled = false;
    }
}