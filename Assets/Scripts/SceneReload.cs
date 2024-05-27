using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    LevelGold levelGold;

    void Start()
    {
        levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
    }
        void Update()
    {
        // Überprüfen, ob die R-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelGold.WinGame();
            
        }
                if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Das Spiel beenden
            Application.Quit();
        }
    }
}

