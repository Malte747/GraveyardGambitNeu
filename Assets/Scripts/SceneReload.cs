using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReloadScene : MonoBehaviour
{
    LevelGold levelGold;
    PasueMenu pauseMenu;
    PlayerMovement playerMovement;
    MsuicManager musicManager;
    Upgrades upgrades;
    SzeneManager szeneManager;
    private GameObject player;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text levelGoldEndText;
    private CharacterController playerController;
    private int levelEndGold;
    private float duration = 3f;
    private bool enterExit = false;
    [SerializeField] private AudioClip[] WinSound;
    [SerializeField] private AudioClip WinSoundFirstRound;
    void Start()
    {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        pauseMenu = GameObject.Find("GM").GetComponent<PasueMenu>();
        player = GameObject.Find("Player(Clone)");
        playerController = player.GetComponent<CharacterController>();
        playerMovement = GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>();
         musicManager = GameObject.Find("MusicManager").GetComponent<MsuicManager>();
         upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
         szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !enterExit)
        {
            enterExit = true;
            levelGold.StopEndTimer();
            musicManager.EndAllMusic();
            StartCoroutine(DisableController());
            playerMovement.EnableBlackscreen();
            endScreen.SetActive(true);
            levelEndGold = levelGold.levelGold;
            StartCoroutine(ExecuteAfterDelay());
            pauseMenu.DisablePauseToggle();
            musicManager.PlayWinSound();
            int randomSound = Random.Range(0, 100);
            if (upgrades.raidcount == 1)
            {
                VoicesManager.instance.PlayVoicesClipOneTime(WinSoundFirstRound, transform, 1f);
            }
            else if(randomSound <= 30)
            {
                VoicesManager.instance.PlayRandomVoicesFXClip(WinSound, transform, 1f);    
            }

        }
    }

    private IEnumerator CountToTargetNumber(int target, float duration)
    {
        int startNumber = 0;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); 
            int currentNumber = Mathf.RoundToInt(Mathf.Lerp(startNumber, target, t));
            levelGoldEndText.text = currentNumber.ToString() + " Gold erbeutet";
            yield return null;
        }

        levelGoldEndText.text = target.ToString() + " Gold erbeutet"; 
    }

        private IEnumerator DisableController()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(CountToTargetNumber(levelEndGold, duration));
        playerController.enabled = false;
    }
    private IEnumerator ExecuteAfterDelay()
    {
        upgrades.AddGold();
        szeneManager.RaidCount();
        yield return new WaitForSeconds(8f);
        levelGold.WinGame();
    }
}

