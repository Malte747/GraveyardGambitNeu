using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsuicManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource alarmMusic;



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

}
