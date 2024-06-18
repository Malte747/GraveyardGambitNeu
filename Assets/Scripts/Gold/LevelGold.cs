using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelGold : MonoBehaviour
{

    public int levelGold = 0;
    public int endTimerChance;
    private bool endTimerStarted = false;
    private Coroutine countdownCoroutine;

    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
  
    
    private TMP_Text levelGoldText;
    [SerializeField] private GameObject EndTimerText;
    [SerializeField] private TMP_Text EndTimeText;
    Upgrades upgrades;
    EnemyAi enemyAi;
    MsuicManager musicManager;
    SzeneManager szeneManager;


    void Start ()
    {
        levelGold = 0;
        levelGoldText = GameObject.Find("LevelGoldText").GetComponent<TextMeshProUGUI>();
        enemyAi = GameObject.Find("Waerter").GetComponent<EnemyAi>();
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MsuicManager>();
        levelGoldText.text = "" + levelGold;
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        endTimerChance = -5 + szeneManager.raid * 3 - upgrades.endTimerDecrease;
    }

    public void HigherEndTimerChance()
    {
        endTimerChance = endTimerChance + 5;
        Debug.Log(endTimerChance);
    }
    public void EndTimer()
    {
        if (!endTimerStarted)
        {
            endTimerStarted = true;
            countdownCoroutine = StartCoroutine(CountdownCoroutine());
            EndTimerText.SetActive(true);
            if(upgrades.raidcount <= 3)
            {
                StartCoroutine(BeamWithDelay(5f));

            }
        }
    }

    private IEnumerator BeamWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
                light1.gameObject.SetActive(true);
                light2.gameObject.SetActive(true);
                light3.gameObject.SetActive(true);
    }

    public void StopEndTimer()
    {
        if (countdownCoroutine != null)
        {
        StopCoroutine(CountdownCoroutine());
        countdownCoroutine = null;
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        float countdownTime = 60f;
        musicManager.StartAlarm();

        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            EndTimeText.text = countdownTime.ToString();
            if (countdownCoroutine == null)
            {
                yield break;
            }
        }
        enemyAi.AttackPlayer();
    }

    public void IncreaseGold(int getGold)
    {
        
        levelGold += getGold;
        levelGoldText.text = "" + levelGold;
    }   

    public void Aufgeben()
    {
        enemyAi.AttackPlayer();
    }

    public void LoseGame()
    {
        levelGold = 0;
        levelGoldText.text = "" + levelGold;
        szeneManager.LoadMenu();
        upgrades.LoseALife();
    }

    public void WinGame()
    {
        upgrades.AddGold();
        szeneManager.RaidCount();
        szeneManager.LoadMenu();
    }




}
