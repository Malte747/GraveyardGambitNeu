using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    void Awake()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab not assigned!");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points array is empty!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
