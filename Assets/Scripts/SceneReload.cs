using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReloadScene : MonoBehaviour
{
    LevelGold levelGold;
    private GameObject player;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text levelGoldEndText;
    private CharacterController playerController;
    private int levelEndGold;
    void Start()
    {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
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
            levelGoldEndText.text = levelEndGold + " Gold erbeutet";
            StartCoroutine(ExecuteAfterDelay());
        }
    }

        private IEnumerator DisableController()
    {
        yield return new WaitForSeconds(3f);
        playerController.enabled = false;
    }
    private IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(7f);
        levelGold.WinGame();
    }
}

