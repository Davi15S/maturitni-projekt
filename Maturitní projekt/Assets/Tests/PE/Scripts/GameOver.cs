using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GoBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
