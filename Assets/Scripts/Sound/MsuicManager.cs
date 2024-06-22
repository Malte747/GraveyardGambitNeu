using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsuicManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource alarmMusic;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;



    void Start()
    {
        gameMusic.gameObject.SetActive(true);
        
    }

    public void StartAlarm()
    {
        gameMusic.gameObject.SetActive(false);
        alarmMusic.gameObject.SetActive(true);
    }

    public void EndAllMusic()
    {
        gameMusic.gameObject.SetActive(false);
        alarmMusic.gameObject.SetActive(false);
    }

    public void PlayWinSound()
    {
        winSound.gameObject.SetActive(true);
    }

    public void PlayLoseSound()
    {
        loseSound.gameObject.SetActive(true);
    }

}
