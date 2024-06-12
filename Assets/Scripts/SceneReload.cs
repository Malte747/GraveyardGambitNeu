using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReloadScene : MonoBehaviour
{
    LevelGold levelGold;
    PasueMenu pauseMenu;
    private GameObject player;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text levelGoldEndText;
    private CharacterController playerController;
    private int levelEndGold;
    private float duration = 3f;
    void Start()
    {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
        pauseMenu = GameObject.Find("GM").GetComponent<PasueMenu>();
        player = GameObject.Find("Player(Clone)");
        playerController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            levelGold.StopEndTimer();
            StartCoroutine(DisableController());
            endScreen.SetActive(true);
            levelEndGold = levelGold.levelGold;
            
            // levelGoldEndText.text = levelEndGold + " Gold erbeutet";
            StartCoroutine(ExecuteAfterDelay());
            pauseMenu.DisablePauseToggle();
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
        yield return new WaitForSeconds(8f);
        levelGold.WinGame();
    }
}

