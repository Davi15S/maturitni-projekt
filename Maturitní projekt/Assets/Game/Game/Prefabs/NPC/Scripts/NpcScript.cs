using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum Subject { Math, PE, Robotics, Programming, Network, Czech }
public class NpcScript : MonoBehaviour
{
    [SerializeField] private Animator visualCueAnimation;
    private TextMeshPro floatingName;
    [SerializeField] public NPCObject npc;
    private bool isDisabled;
    void Start()
    {
        floatingName = GetComponentInChildren<TextMeshPro>();
        floatingName.text = npc.name;
    }

    void Update()
    {
        if (visualCueAnimation.GetBool("playerInRange") && Input.GetKeyDown(KeyCode.E) && !isDisabled)
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(npc.subject.ToString());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !isDisabled)
        {
            visualCueAnimation.SetBool("playerInRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !isDisabled)
        {
            visualCueAnimation.SetBool("playerInRange", false);
        }
    }
    public void SetHidden()
    {
        this.gameObject.SetActive(false);
    }
    public Subject GetSubject()
    {
        return npc.subject;
    }
    public string GetName()
    {
        return npc.name;
    }
    public void SetActive()
    {
        this.gameObject.SetActive(true);
    }
    public void SetDisabled()
    {
        Debug.Log($"Task is already finished! | Subject: {npc.subject} | Name: {npc.name}");
        isDisabled = true;
    }
}
