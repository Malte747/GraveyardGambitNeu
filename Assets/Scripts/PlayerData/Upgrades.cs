using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public static Upgrades instance;
    SzeneManager szeneManager;
    private const string PlayerGoldKey = "PlayerGold";
    private const string PlayerHealthKey = "PlayerHealth";
    [SerializeField] LevelGold levelGold;
    [SerializeField] private TMP_Text GlobalGoldText;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text raidcounter;
    

    public int GlobalGold = 0;
    public int health = 3;
    private int raidcount = 0;


    // Upgrades
    public int increaseChance = 0;



    private void Awake()
    {
        
        if (instance == null)
        {
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GlobalGold = PlayerPrefs.GetInt(PlayerGoldKey, 0);
        health = PlayerPrefs.GetInt(PlayerHealthKey, 3);
        SceneManager.sceneLoaded += OnSceneLoaded;
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        
    }

        void Start()
        {
            HealthText.text = "Health: " + health;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HealthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        if (scene.buildIndex == 1)
        {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        HealthText.text = "Health: " + health;
        }
        else
        {
        GlobalGoldText = GameObject.Find("GlobalGoldText").GetComponent<TextMeshProUGUI>();
        raidcounter = GameObject.Find("Überfall").GetComponent<TextMeshProUGUI>();
        GlobalGoldText.text = "Gold: " + GlobalGold;
        raidcount = szeneManager.raid + 1;
        raidcounter.text = "Raubzug Nr " + raidcount;
        HealthText.text = "Health: " + health;
        }

        
        
    }
    public void SaveGold()
    {
        PlayerPrefs.SetInt(PlayerGoldKey, GlobalGold);
        PlayerPrefs.SetInt(PlayerHealthKey, health);
        PlayerPrefs.Save();
    }

    public void AddGold()
    {
        GlobalGold = GlobalGold + levelGold.levelGold;
        SaveGold();
        GlobalGoldText.text = "Gold: " + GlobalGold;
    }

    public void LoseALife()
    {
        health = health - 1;
        
        if (health == 0)
        {
            Reset();
        }
        SaveGold();
    }

    public void Reset()
    {
        GlobalGold = 0;
        health  = 3;
        SaveGold();
        SzeneManager.instance.raid = 0;
        SzeneManager.instance.SaveRaid();
        GlobalGoldText.text = "Gold: " + GlobalGold;
        raidcount = szeneManager.raid + 1;
        raidcounter.text = "Raubzug Nr " + raidcount;
        HealthText.text = "Health: " + health;
        
    }

}
