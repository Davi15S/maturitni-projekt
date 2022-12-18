using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    void Update()
    {
        transform.position += Vector3.left * PEManager.instance.GetGameSpeed() * Time.deltaTime;
        if (transform.position.x < leftEdge)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PEManager.instance.GameOver();
        }
    }
}
