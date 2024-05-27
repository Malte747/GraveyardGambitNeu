using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveInteract : MonoBehaviour, IInteractable
{
         LevelGold levelGold;
         Interactor interactor;
        public float getGoldPercent = 69f;
        public Canvas canvastext;
        public GameObject particlesystem;
        public GameObject particlesystemCommon;
        public GameObject particlesystemMedium;
        public GameObject particlesystemRare;
        public GameObject particlesystemEpic;
        public GameObject particlesystemLegendary;
        public int increaseChance;
        Upgrades upgrades;
        

        void Start()
        {
            levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
            interactor = GameObject.Find("Cam_holder").GetComponent<Interactor>();
            canvastext.gameObject.SetActive(false);
            upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
            increaseChance = upgrades.increaseChance;
        }

        void Update()
        {
            if (interactor.seeTarget == false)
            {
                canvastext.gameObject.SetActive(false);
            }
           
        }
    	public void Interact()
        {
            if (levelGold != null)
            {
                
                    int randomAmount = Random.Range(0, 100);
                    if (randomAmount <= getGoldPercent)
                    {
                        int randomChance = Random.Range(0, 100);
                        int finalChance = randomChance + increaseChance;

                        if (finalChance <= 35)
                        {
                            levelGold.IncreaseGold(10);
                            particlesystemCommon.gameObject.SetActive(true);
                            Debug.Log("Common " + randomChance + " " + finalChance);
                        }
                    else if (finalChance <= 65)
                        {
                            levelGold.IncreaseGold(15);
                            particlesystemMedium.gameObject.SetActive(true);
                            Debug.Log("Medium " + randomChance + " " + finalChance);
                        }
                    else if (finalChance <= 85)
                        {
                            levelGold.IncreaseGold(25);
                            particlesystemRare.gameObject.SetActive(true);
                            Debug.Log("Rare " + randomChance + " " + finalChance);
                        }
                    else if (finalChance <= 95)
                        {
                            levelGold.IncreaseGold(35);
                            particlesystemEpic.gameObject.SetActive(true);
                            Debug.Log("Epic " + randomChance + " " + finalChance);
                        }
                    else 
                        {
                            levelGold.IncreaseGold(50);
                            particlesystemLegendary.gameObject.SetActive(true);
                            Debug.Log("Legendary " + randomChance + " " + finalChance);
                        }

                    }
                    else
                    {
                        Debug.Log("Leider kein Gold für dich");
                    }
                
            }
            particlesystem.gameObject.SetActive(true);
            Destroy(gameObject);
        }

        public void Visible()
        {
             canvastext.gameObject.SetActive(true);
        }

        public void InVisible()
        {
             canvastext.gameObject.SetActive(false);
        }
}
