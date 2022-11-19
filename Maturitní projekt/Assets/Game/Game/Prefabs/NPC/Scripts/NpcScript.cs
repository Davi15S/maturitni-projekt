using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcScript : MonoBehaviour
{
    [SerializeField] private Animator visualCueAnimation;
    private TextMeshPro floatingName;
    [SerializeField] private string npcName;

    void Start()
    {
        floatingName = GetComponentInChildren<TextMeshPro>();
        floatingName.text = npcName;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") { visualCueAnimation.SetBool("playerInRange", true); }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") { visualCueAnimation.SetBool("playerInRange", false); }
    }
}
