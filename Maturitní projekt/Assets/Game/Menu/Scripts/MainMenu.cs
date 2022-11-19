using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    private void Start() { if (!DataPersistenceManager.instance.HasGameData()) { continueGameButton.interactable = false; } }
    public void LoadNewGame()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Game");
    }
    public void LoadSavedGame()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Game");
    }
    public void Quit() { Application.Quit(); }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
