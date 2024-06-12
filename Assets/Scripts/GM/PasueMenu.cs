using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasueMenu : MonoBehaviour
{
    HeadBob headbob;
    [SerializeField] private bool isPaused = false;
    private bool pauseToggle = true;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        headbob = GameObject.Find("Player(Clone)").GetComponent<HeadBob>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseToggle) 
        {
            TogglePause();     
        }
    }

    public void DisablePauseToggle()
    {
        pauseToggle = false;
    }
    
     public void TogglePause()
    {
        isPaused = !isPaused; 

        if (isPaused)
        {
            Time.timeScale = 0f;
            headbob.DisableHeadBob();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
            pauseMenu.gameObject.SetActive(true);
            Debug.Log("Spiel pausiert");
        }
        else
        {
            Time.timeScale = 1f; 
            headbob.EnableHeadBob();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.gameObject.SetActive(false);
            Debug.Log("Spiel fortgesetzt");
        }
    }

}
