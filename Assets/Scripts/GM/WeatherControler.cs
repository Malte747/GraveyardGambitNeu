using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherControler : MonoBehaviour
{
    public GameObject[] gameObjects;
    Upgrades upgrades;   
    
    
    void Start()
    {
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        if (upgrades.raidcount >= 2)
        {
        RollDiceAndActivateObject();
        }
    }

    void RollDiceAndActivateObject()
    {
        
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }

        
        int diceRoll = Random.Range(0, 3);

        if (diceRoll == 0) 
        {
            
            int randomIndex = Random.Range(0, gameObjects.Length);
            gameObjects[randomIndex].SetActive(true);
        }
    }

}
