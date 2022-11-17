using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() { SceneManager.LoadScene("Floor0"); }
    public void Quit() { Application.Quit(); }
}
