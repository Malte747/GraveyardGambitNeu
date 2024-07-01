using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    SzeneManager szeneManager;
    Upgrades upgrades;
    public GameObject playerPrefab;
    public GameObject BigGrave;
    public Transform[] spawnPoints;
    public Transform[] spawnPointsBigGrave;
    [SerializeField] private GameObject[] ExitToActivate;
    [SerializeField] private GameObject gateDeactivate;
     [SerializeField] private GameObject treeDeactivate;
     [SerializeField] private GameObject wind1;
     [SerializeField] private GameObject wind2;
     [SerializeField] private GameObject wind3;


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

        if (BigGrave == null)
        {
            Debug.LogError("Grave Prefab not assigned!");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Grave Spawn points array is empty!");
            return;
        }

        int randomIndexGrave = Random.Range(0, spawnPointsBigGrave.Length);
        Transform spawnPointGrave = spawnPointsBigGrave[randomIndexGrave];
        Instantiate(BigGrave, spawnPointGrave.position, spawnPointGrave.rotation);
        
    }

    void Start()
    {
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        if (upgrades.openGates >= 1)
        {
            upgrades.DecimateOpenGates();
        }
        else
        {
        if (szeneManager.raid >= 4 && szeneManager.raid <= 11)
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
            wind1.SetActive(false);
        }
        else if (randomIndex == 1)
        {
             wind2.SetActive(false);
        }
        else if (randomIndex == 2)
        {
            treeDeactivate.SetActive(false);
             wind3.SetActive(false);
        }
        }

        else if (szeneManager.raid >= 12 )
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
            wind1.SetActive(false);
        }
        if (index1 == 1 || index2 == 1)
        {
            wind2.SetActive(false);
        }
        if (index1 == 2 || index2 == 2)
        {
            treeDeactivate.SetActive(false);
            wind3.SetActive(false);
        }
        }
        }
    }
}
