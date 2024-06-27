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
    private bool endTimerSoundStarted = false;
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

    // Sound

    [SerializeField] private AudioClip[] firstRoundStart;
    [SerializeField] private AudioClip[] endTimerVoice;
    [SerializeField] private AudioClip[] FirstExitClosed;
    [SerializeField] private AudioClip[] SecondExitClosed;
    [SerializeField] private AudioClip[] BeamActiveSound;
    [SerializeField] private AudioClip[] BeamInactiveSound;
    [SerializeField] private AudioClip[] BallonSound;


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
        if (upgrades.raidcount == 1)
        {
            StartCoroutine(FirstRoundSoundWithDelay(4));
        }
        if (upgrades.raidcount == 5)
        {
            VoicesManager.instance.PlayRandomVoicesFXClip(FirstExitClosed, transform, 1f);
        }
        if (upgrades.raidcount == 8)
        {
            VoicesManager.instance.PlayRandomVoicesFXClip(BallonSound, transform, 1f);
        }
        if (upgrades.raidcount == 13)
        {
            VoicesManager.instance.PlayRandomVoicesFXClip(SecondExitClosed, transform, 1f);
        }
    }

    public void HigherEndTimerChance()
    {
        endTimerChance = endTimerChance + 5;
        Debug.Log(endTimerChance);
    }

    public void EndTimerDelay()
    {
        if (!endTimerSoundStarted)
        {
        endTimerSoundStarted = true;
        VoicesManager.instance.PlayRandomVoicesFXClip(endTimerVoice, transform, 1f);
        StartCoroutine(ExecuteEndTimerAfterDelay(6f));
        }
    }
    public void EndTimer()
    {
        if (!endTimerStarted)
        {
            endTimerStarted = true;
            endTimerSoundStarted = true;
            countdownCoroutine = StartCoroutine(CountdownCoroutine());
            EndTimerText.SetActive(true);
            if(upgrades.raidcount <= 3)
            {
                StartCoroutine(BeamWithDelay(10f));
                StartCoroutine(BeamActiveSoundWithDelay(10f));
            }
        if (upgrades.raidcount >= 4 && upgrades.inactiveBeam == 0)
            {
            upgrades.InactiveBeamSet();
            StartCoroutine(BeamInactiveSoundDelay(7f));
            }
        }
    }

    private IEnumerator ExecuteEndTimerAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            EndTimer();
        }

    private IEnumerator BeamWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
                light1.gameObject.SetActive(true);
                light2.gameObject.SetActive(true);
                light3.gameObject.SetActive(true);
    }
        private IEnumerator BeamInactiveSoundDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        VoicesManager.instance.PlayRandomVoicesFXClip(BeamInactiveSound, transform, 1f);
    }
    private IEnumerator BeamActiveSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        VoicesManager.instance.PlayRandomVoicesFXClip(BeamActiveSound, transform, 1f);
    }
    private IEnumerator FirstRoundSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        VoicesManager.instance.PlayRandomVoicesFXClip(firstRoundStart, transform, 1f);
        
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
        szeneManager.LoadMenu();
    }




}
