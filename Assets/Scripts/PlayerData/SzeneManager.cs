using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SzeneManager : MonoBehaviour
{
    public static SzeneManager instance {get; private set;}
    public int raid = 0;
    private const string PlayerRaid = "PlayerRaid";


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
        raid = PlayerPrefs.GetInt(PlayerRaid, 0);
    }

    public void SaveRaid()
    {
        PlayerPrefs.SetInt(PlayerRaid, raid);
        PlayerPrefs.Save();
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ResetGame()
    {
        Upgrades.instance.Reset();
    }

    public void RaidCount()
    {
        raid++;
        SaveRaid();
    }


}
