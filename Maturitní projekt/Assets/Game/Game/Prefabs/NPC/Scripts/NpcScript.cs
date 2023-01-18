using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum Subject { Math, PE, Robotics, Programming }
public class NpcScript : MonoBehaviour
{
    [SerializeField] private Animator visualCueAnimation;
    private TextMeshPro floatingName;
    [SerializeField] public NPCObject npc;

    void Start()
    {
        floatingName = GetComponentInChildren<TextMeshPro>();
        floatingName.text = npc.name;
    }

    void Update()
    {
        if (visualCueAnimation.GetBool("playerInRange") && Input.GetKeyDown(KeyCode.E))
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(npc.subject.ToString());
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
