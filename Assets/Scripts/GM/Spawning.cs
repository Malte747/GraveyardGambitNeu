using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    SzeneManager szeneManager;
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    [SerializeField] private GameObject[] ExitToActivate;
    [SerializeField] private GameObject gateDeactivate;
     [SerializeField] private GameObject treeDeactivate;


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

    void Start()
    {
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();

        if (szeneManager.raid >= 3 && szeneManager.raid <= 5)
        {
        foreach (GameObject obj in ExitToActivate)
        {
            obj.SetActive(false);
        }

        // Randomly select one of the objects and activate it
        int randomIndex = Random.Range(0, ExitToActivate.Length);
        ExitToActivate[randomIndex].SetActive(true);
        if (randomIndex == 0)
        {
            gateDeactivate.SetActive(false);
        }
        else if (randomIndex == 2)
        {
            treeDeactivate.SetActive(false);
        }
        }

        else if (szeneManager.raid >= 6 )
        {
        foreach (GameObject obj in ExitToActivate)
        {
            obj.SetActive(false);
        }

        int index1 = Random.Range(0, ExitToActivate.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, ExitToActivate.Length);
        } while (index1 == index2);

        // Activate the two selected objects
        ExitToActivate[index1].SetActive(true);
        ExitToActivate[index2].SetActive(true);
        if (index1 == 0 || index2 == 0)
        {
            gateDeactivate.SetActive(false);
        }
        if (index1 == 2 || index2 == 2)
        {
            treeDeactivate.SetActive(false);
        }
        }
    }
}
