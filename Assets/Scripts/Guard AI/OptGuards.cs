using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptGuards : MonoBehaviour
{
    public int round;
    SzeneManager szeneManager;
    void Start()
    {
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        if (szeneManager.raid < round)
        {
            this.gameObject.SetActive(false);
        }
    }


}
