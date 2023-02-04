using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CzechTimer : MonoBehaviour
{
    private float gameTime;

    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        gameTime = 5f;
    }

    void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            CzechManager.instance.SetQuestion();
            this.gameObject.SetActive(false);
        }
    }
}
