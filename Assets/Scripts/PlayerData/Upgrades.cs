using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Upgrades : MonoBehaviour
{
    public static Upgrades instance;
    SzeneManager szeneManager;
    private const string PlayerGoldKey = "PlayerGold";
    private const string PlayerHealthKey = "PlayerHealth";
    private const string PlayerStaminaKey = "PlayerStamina";
    private const string LevelKey = "CurrentLevel";
    private const string GoldChanceKey = "GoldChanceIncrease";
    private const string GoldIncreaseKey = "GoldChanceIncreaseCurrentLevel";
    private const string EndTimerDecreaseKey = "EndTimerDecrease";
    private const string EndTimerDecreaseLevelKey = "EndTimerDecreaseCurrentLevel";
    private const string holdDurationKey = "HoldDuration";
    private const string holdDurationLevelKey = "HoldDurationCurrentLevel";
    LevelGold levelGold;
    [SerializeField] HealthManager hearts;
    [SerializeField] private TMP_Text GlobalGoldText;
    [SerializeField] private TMP_Text raidcounter;
    

    public int GlobalGold = 0;
    public int health = 3;
    private int raidcount = 0;


    // Upgrades
    
    private readonly int[] UpgradeCost = { 100, 200, 300, 500, 800};
     private const int maxLevel = 5;

    // stamina Upgrade

    public int stamina { get; private set; } = 0;
    private readonly int[] staminaValues = { 0, 20, 40, 60, 80, 120 };
    [SerializeField] public int staminacurrentLevel = 0;

    // GoldChance Update
    public int increaseChance = 0;
    private readonly int[] goldChanceIncrease = { 0, 2, 4, 6, 8, 12 };
    [SerializeField] public int goldChanceIncreasecurrentLevel = 0;

    // EndTimerDecrease Upgrade

    public int endTimerDecrease = 0;
    private readonly int[] endTimerDecreaseValues = { 0, 10, 20, 30, 40, 60 };
    [SerializeField] public int endTimerDecreasecurrentLevel = 0;

    // HoldDuration Upgrade

    public int holdDuration = 40;
    private readonly int[] holdDurationValues = { 40, 30, 25, 20, 15, 5 };
    [SerializeField] public int holdDurationcurrentLevel = 0;

    // Buy a Heart

    public int heartCost = 1000;



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
        
        LoadData();
        SceneManager.sceneLoaded += OnSceneLoaded;
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        
    }




        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.buildIndex == 1)
        {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        }
        else
        {
        hearts = GameObject.Find("GM").GetComponent<HealthManager>();
        raidcounter = GameObject.Find("Überfall").GetComponent<TextMeshProUGUI>();
        raidcount = szeneManager.raid + 1;
        GlobalGoldText = GameObject.Find("GlobalGoldText").GetComponent<TextMeshProUGUI>();
        GlobalGoldText.text = "Gold: " + GlobalGold;
        raidcounter.text = "Raubzug Nr " + raidcount;
        
        }

        
        
    }
    private void LoadData()
    {
        GlobalGold = PlayerPrefs.GetInt(PlayerGoldKey, 0);
        health = PlayerPrefs.GetInt(PlayerHealthKey, 3);
        stamina = PlayerPrefs.GetInt(PlayerStaminaKey, 0);
        staminacurrentLevel = PlayerPrefs.GetInt(LevelKey, 0);
        goldChanceIncreasecurrentLevel = PlayerPrefs.GetInt(GoldChanceKey, 0);
        increaseChance = PlayerPrefs.GetInt(GoldIncreaseKey, 0);
        endTimerDecrease = PlayerPrefs.GetInt(EndTimerDecreaseKey, 0);
        endTimerDecreasecurrentLevel = PlayerPrefs.GetInt(EndTimerDecreaseLevelKey, 0);
        holdDuration = PlayerPrefs.GetInt(holdDurationKey, 0);
        holdDurationcurrentLevel = PlayerPrefs.GetInt(holdDurationLevelKey, 0);
    }
    
    public void SaveGold()
    {
        PlayerPrefs.SetInt(PlayerGoldKey, GlobalGold);
        PlayerPrefs.SetInt(PlayerHealthKey, health);
        PlayerPrefs.SetInt(PlayerStaminaKey, stamina);
        PlayerPrefs.SetInt(LevelKey, staminacurrentLevel);
        PlayerPrefs.SetInt(GoldChanceKey, goldChanceIncreasecurrentLevel);
        PlayerPrefs.SetInt(GoldIncreaseKey, increaseChance);
        PlayerPrefs.SetInt(EndTimerDecreaseKey, endTimerDecrease);
        PlayerPrefs.SetInt(EndTimerDecreaseLevelKey, endTimerDecreasecurrentLevel);
        PlayerPrefs.SetInt(holdDurationKey, holdDuration);
        PlayerPrefs.SetInt(holdDurationLevelKey,holdDurationcurrentLevel);
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
        if (health > 1)
        {
        health--;
        }
        else if (health == 1)
        {
            Reset();
        }
        SaveGold();
    }

    public void GainALife()
    {
        if ( GlobalGold >= heartCost && health < 3 )
        {
            health++;
            GlobalGold = GlobalGold - heartCost;
            GlobalGoldText.text = "Gold: " + GlobalGold;
            hearts.GainLife();
        }
        else
        {
            Debug.Log("Already Max Health");
        }
        SaveGold();
    }



    public void Reset()
    {
        SzeneManager.instance.raid = 0;
        SzeneManager.instance.SaveRaid();
        GlobalGold = 0;
        health  = 3;
        stamina = 0;
        increaseChance = 0;
        staminacurrentLevel = 0;
        goldChanceIncreasecurrentLevel = 0;
        endTimerDecrease = 0;
        endTimerDecreasecurrentLevel = 0;
        holdDuration = 40;
        holdDurationcurrentLevel = 0;
        SaveGold();
        hearts.UpdateHearts();
        GlobalGoldText.text = "Gold: " + GlobalGold;
        raidcount = szeneManager.raid + 1;
        raidcounter.text = "Raubzug Nr " + raidcount;
        
    }

    public void UpgradeStamina()
    {
        if (staminacurrentLevel < maxLevel && GlobalGold >= UpgradeCost[staminacurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[staminacurrentLevel];
            GlobalGoldText.text = "Gold: " + GlobalGold;
            staminacurrentLevel++;
            stamina = staminaValues[staminacurrentLevel];
            SaveGold();
        }
        else
        {
            Debug.Log("Stamina is already at max level or too expensive.");
        }
    }

    public void UpgradeGoldChance()
    {
        if (goldChanceIncreasecurrentLevel < maxLevel && GlobalGold >= UpgradeCost[goldChanceIncreasecurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[goldChanceIncreasecurrentLevel];
            GlobalGoldText.text = "Gold: " + GlobalGold;
            goldChanceIncreasecurrentLevel++;
            increaseChance = goldChanceIncrease[goldChanceIncreasecurrentLevel];
            SaveGold();
        }
        else
        {
            Debug.Log("Gold Chance is already at max level or too expensive.");
        }
    }

        public void UpgradeEndTimerDecrease()
    {
        if (endTimerDecreasecurrentLevel < maxLevel && GlobalGold >= UpgradeCost[endTimerDecreasecurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[endTimerDecreasecurrentLevel];
            GlobalGoldText.text = "Gold: " + GlobalGold;
             endTimerDecreasecurrentLevel++;
             endTimerDecrease = endTimerDecreaseValues[endTimerDecreasecurrentLevel];
            SaveGold();
        }
        else
        {
            Debug.Log("End timer Decrease is already at max level or too expensive.");
        }
    }

        public void UpgradeHoldTimer()
    {
        if (holdDurationcurrentLevel < maxLevel && GlobalGold >= UpgradeCost[holdDurationcurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[holdDurationcurrentLevel];
            GlobalGoldText.text = "Gold: " + GlobalGold;
             holdDurationcurrentLevel++;
             holdDuration = holdDurationValues[holdDurationcurrentLevel];
            SaveGold();
        }
        else
        {
            Debug.Log("Hold Timer is already at max level or too expensive.");
        }
    }

}
