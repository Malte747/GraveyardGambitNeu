using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Upgrades upgrades;
    public int health;
    public Image[] hearts; 

    private void Start() 
    {
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        health = upgrades.health;
        UpdateHearts();
    }


    public void UpdateHearts()
    {
        health = upgrades.health;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true; 
            }
            else
            {
                hearts[i].enabled = false; 
            }
        }
    }

    public void GainLife()
    {
        if (health < hearts.Length)
        {
            health++;
            UpdateHearts();
        }
    }
}

