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
  
    
    private TMP_Text levelGoldText;
    [SerializeField] private GameObject EndTimerText;
    [SerializeField] private TMP_Text EndTimeText;
    Upgrades upgrades;
    EnemyAi enemyAi;
    SzeneManager szeneManager;


    void Start ()
    {
        levelGold = 0;
        levelGoldText = GameObject.Find("LevelGoldText").GetComponent<TextMeshProUGUI>();
        enemyAi = GameObject.Find("Wärter").GetComponent<EnemyAi>();
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        levelGoldText.text = "Gold: " + levelGold;
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        endTimerChance = 0 + szeneManager.raid * 5;
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
        }
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
        levelGoldText.text = "Gold: " + levelGold;
    }   

    public void LoseGame()
    {
        levelGold = 0;
        levelGoldText.text = "Gold: " + levelGold;
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
