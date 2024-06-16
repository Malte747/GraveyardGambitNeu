using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveInteract : MonoBehaviour, IInteractable
{
         LevelGold levelGold;
         Interactor interactor;
        public float getGoldPercent = 79f;
        public Canvas canvastext;
        public GameObject particlesystem;
        public GameObject particlesystemCommon;
        public GameObject particlesystemMedium;
        public GameObject particlesystemRare;
        public GameObject particlesystemEpic;
        public GameObject particlesystemLegendary;
        [SerializeField] private AudioClip GraveOpen; 
        [SerializeField] private AudioClip common;
        [SerializeField] private AudioClip medium; 
        [SerializeField] private AudioClip rare; 
        [SerializeField] private AudioClip epic; 
        [SerializeField] private AudioClip Legendary; 
        [SerializeField] private AudioClip empty; 
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
                    if (randomAmount <= getGoldPercent + increaseChance)
                    {
                    int randomCountdown = Random.Range(0, 100);
                    if (randomCountdown <= levelGold.endTimerChance)
                    {
                        levelGold.EndTimer();
                    }
                    levelGold.HigherEndTimerChance();
                    int randomChance = Random.Range(0, 100);

                    if (randomChance <= 50 - increaseChance*4)
                        {
                            levelGold.IncreaseGold(15);
                            particlesystemCommon.gameObject.SetActive(true);
                            SoundFXManager.instance.PlaySoundFXClip(common, transform, 0.7f);
                            Debug.Log("Common " + randomChance + " " + increaseChance*4);
                        }
                    else if (randomChance <= 78 - increaseChance*3)
                        {
                            levelGold.IncreaseGold(20);
                            particlesystemMedium.gameObject.SetActive(true);
                            SoundFXManager.instance.PlaySoundFXClip(medium, transform, 0.8f);
                            Debug.Log("Medium " + randomChance + " " + increaseChance*3);
                        }
                    else if (randomChance <= 93 - increaseChance*2)
                        {
                            levelGold.IncreaseGold(30);
                            particlesystemRare.gameObject.SetActive(true);
                            SoundFXManager.instance.PlaySoundFXClip(rare, transform, 0.8f);
                            Debug.Log("Rare " + randomChance + " " + increaseChance*2);
                        }
                    else if (randomChance <= 98 - increaseChance*1)
                        {
                            levelGold.IncreaseGold(40);
                            particlesystemEpic.gameObject.SetActive(true);
                            SoundFXManager.instance.PlaySoundFXClip(epic, transform, 0.9f);
                            Debug.Log("Epic " + randomChance + " " + increaseChance*1);
                        }
                    else 
                        {
                            levelGold.IncreaseGold(60);
                            particlesystemLegendary.gameObject.SetActive(true);
                            SoundFXManager.instance.PlaySoundFXClip(Legendary, transform, 0.9f);
                            Debug.Log("Legendary " + randomChance );
                        }

                    }
                    else
                    {
                        Debug.Log("Leider kein Gold für dich");
                        SoundFXManager.instance.PlaySoundFXClip(empty, transform, 0.7f);
                    }
                
            }
            particlesystem.gameObject.SetActive(true);
            SoundFXManager.instance.PlaySoundFXClipOneTime(GraveOpen, transform, 0.7f);
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
