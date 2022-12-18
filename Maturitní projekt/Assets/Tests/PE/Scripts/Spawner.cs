using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    private void OnEnable()
    {
        InvokeSpawn();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        GameObject obstacle = Instantiate(prefab);
        obstacle.transform.position += transform.position;
        InvokeSpawn();
    }

    private void InvokeSpawn()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
