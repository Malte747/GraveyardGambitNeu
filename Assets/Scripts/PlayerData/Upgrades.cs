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
    ButtonScript buttonscript;
    private const string HighScoreKey = "HighScore";
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
    private const string addSpeedKey = "AddSpeed";
    private const string addSpeedLevelKey = "AddSpeedCurrentLevel";
    private const string OpenGatesKey = "OpenGates";
    LevelGold levelGold;
    [SerializeField] HealthManager hearts;
    [SerializeField] private TMP_Text GlobalGoldText;
    [SerializeField] private TMP_Text raidcounter;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private AudioClip buttonBuy;
    [SerializeField] private AudioClip heartbeat;
    [SerializeField] private AudioClip gatesopen;
    

    public int GlobalGold = 0;
    public int health = 3;
    public int raidcount = 0;
    [SerializeField] private int highscore = 0;
    private GameObject HighscoreText;
    public bool currentlyPlaying = false;


    // Upgrades
    
    private readonly int[] UpgradeCost = { 50, 100, 150, 300, 500};
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
    private readonly int[] endTimerDecreaseValues = { 0, 10, 20, 30, 50, 80 };
    [SerializeField] public int endTimerDecreasecurrentLevel = 0;

    // HoldDuration Upgrade

    public int holdDuration = 40;
    private readonly int[] holdDurationValues = { 40, 30, 25, 20, 15, 5 };
    [SerializeField] public int holdDurationcurrentLevel = 0;

    // Speed Upgrade

    public int addSpeed = 0;
    private readonly int[] addSpeedValues = { 0, 10, 20, 30, 40, 60};
    [SerializeField] public int addSpeedcurrentLevel = 0;

    // Buy a Heart

    public int heartCost = 100;

    // Open Gates

    public int openGates = 0;
    public int openGatescost = 50;



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
        if(health == 0)
        {
            Reset();
            SaveGold();
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        hearts = GameObject.Find("GM").GetComponent<HealthManager>();
        raidcounter = GameObject.Find("Überfall").GetComponent<TextMeshProUGUI>();
        raidcount = szeneManager.raid + 1;
        GlobalGoldText = GameObject.Find("GlobalGoldText").GetComponent<TextMeshProUGUI>();
        GlobalGoldText.text = "" + GlobalGold;
        raidcounter.text = "Raubzug Nr " + raidcount;
        highscoreText = GameObject.Find("Highscore").GetComponent<TextMeshProUGUI>();
        HighscoreText = GameObject.Find("Highscore");
        highscoreText.text = "Highscore: " + highscore;
        buttonscript = GameObject.Find("GM").GetComponent<ButtonScript>();
        if (raidcount > highscore)
        {
            highscore = raidcount;
            highscoreText.text = "Highscore: " + highscore;
        }
        if (highscore < 2)
        {
            HighscoreText.SetActive(false);
        }
        
        }

        
        
    }
    private void LoadData()
    {
        GlobalGold = PlayerPrefs.GetInt(PlayerGoldKey, 0);
        highscore = PlayerPrefs.GetInt(HighScoreKey, 0);
        health = PlayerPrefs.GetInt(PlayerHealthKey, 3);
        stamina = PlayerPrefs.GetInt(PlayerStaminaKey, 0);
        staminacurrentLevel = PlayerPrefs.GetInt(LevelKey, 0);
        goldChanceIncreasecurrentLevel = PlayerPrefs.GetInt(GoldChanceKey, 0);
        increaseChance = PlayerPrefs.GetInt(GoldIncreaseKey, 0);
        endTimerDecrease = PlayerPrefs.GetInt(EndTimerDecreaseKey, 0);
        endTimerDecreasecurrentLevel = PlayerPrefs.GetInt(EndTimerDecreaseLevelKey, 0);
        holdDuration = PlayerPrefs.GetInt(holdDurationKey, 40);
        holdDurationcurrentLevel = PlayerPrefs.GetInt(holdDurationLevelKey, 0);
        addSpeed = PlayerPrefs.GetInt(addSpeedKey, 0);
        addSpeedcurrentLevel = PlayerPrefs.GetInt(addSpeedLevelKey, 0);
        openGates = PlayerPrefs.GetInt(OpenGatesKey, 0);
    }
    
    public void SaveGold()
    {
        PlayerPrefs.SetInt(HighScoreKey, highscore);
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
        PlayerPrefs.SetInt(addSpeedKey, addSpeed);
        PlayerPrefs.SetInt(addSpeedLevelKey,addSpeedcurrentLevel);
        PlayerPrefs.SetInt(OpenGatesKey,openGates);
        PlayerPrefs.Save();
    }

    public void AddGold()
    {
        GlobalGold = GlobalGold + levelGold.levelGold;
        SaveGold();
    }

    public void LoseALife()
    {
        currentlyPlaying = true;
        health--;
        SaveGold();
    }

    public void GainALife()
    {
        if ( GlobalGold >= heartCost && health < 3 )
        {
            health++;
            GlobalGold = GlobalGold - heartCost;
            GlobalGoldText.text = "" + GlobalGold;
            hearts.GainLife();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
            SoundFXManager.instance.PlaySoundFXClip(heartbeat, transform, 0.5f);
        }
        else
        {
            Debug.Log("Already Max Health");
        }
        SaveGold();
    }

    public void ResetHighscore()
    {
        highscore = 0;
        highscoreText.text = "Highscore: " + highscore;
        HighscoreText.SetActive(false);
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
        addSpeed = 0;
        addSpeedcurrentLevel = 0;
        openGates = 0;
        SaveGold();
        hearts.UpdateHearts();
        GlobalGoldText.text = "" + GlobalGold;
        raidcount = szeneManager.raid + 1;
        raidcounter.text = "Raubzug Nr " + raidcount;
        buttonscript.Reset();
        
    }

    public void UpgradeStamina()
    {
        if (staminacurrentLevel < maxLevel && GlobalGold >= UpgradeCost[staminacurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[staminacurrentLevel];
            GlobalGoldText.text = "" + GlobalGold;
            staminacurrentLevel++;
            stamina = staminaValues[staminacurrentLevel];
            SaveGold();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
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
            GlobalGoldText.text = "" + GlobalGold;
            goldChanceIncreasecurrentLevel++;
            increaseChance = goldChanceIncrease[goldChanceIncreasecurrentLevel];
            SaveGold();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
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
            GlobalGoldText.text = "" + GlobalGold;
             endTimerDecreasecurrentLevel++;
             endTimerDecrease = endTimerDecreaseValues[endTimerDecreasecurrentLevel];
            SaveGold();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
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
            GlobalGoldText.text = "" + GlobalGold;
             holdDurationcurrentLevel++;
             holdDuration = holdDurationValues[holdDurationcurrentLevel];
            SaveGold();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
        }
        else
        {
            Debug.Log("Hold Timer is already at max level or too expensive.");
        }
    }

        public void UpgradeAddSpeed()
    {
        if (addSpeedcurrentLevel < maxLevel && GlobalGold >= UpgradeCost[addSpeedcurrentLevel])
        {
            GlobalGold = GlobalGold - UpgradeCost[addSpeedcurrentLevel];
            GlobalGoldText.text = "" + GlobalGold;
             addSpeedcurrentLevel++;
             addSpeed = addSpeedValues[addSpeedcurrentLevel];
            SaveGold();
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
        }
        else
        {
            Debug.Log("Add Speed is already at max level or too expensive.");
        }
    }

        public void DecimateOpenGates()
    {
        openGates--;
    }

     public void OpenGates()
    {
        if ( GlobalGold >= openGatescost && openGates < 1 )
        {
            openGates++;
            GlobalGold = GlobalGold - openGatescost;
            GlobalGoldText.text = "" + GlobalGold;
            SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
            SoundFXManager.instance.PlaySoundFXClip(gatesopen, transform, 0.5f);
        }
        else
        {
            Debug.Log("Gates already Open");
        }
        SaveGold();
    }
}
