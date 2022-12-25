using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NpcScript : MonoBehaviour
{
    private enum Subject { Math, PE, Robotics }
    [SerializeField] private Animator visualCueAnimation;
    [SerializeField] private Subject subject;
    private TextMeshPro floatingName;
    [SerializeField] private string npcName;

    void Start()
    {
        floatingName = GetComponentInChildren<TextMeshPro>();
        floatingName.text = npcName;
    }

    void Update()
    {
        if (visualCueAnimation.GetBool("playerInRange") && Input.GetKeyDown(KeyCode.E))
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(subject.ToString());
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            visualCueAnimation.SetBool("playerInRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            visualCueAnimation.SetBool("playerInRange", false);
        }
    }
}
