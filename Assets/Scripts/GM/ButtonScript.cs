using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class ButtonScript : MonoBehaviour
{
    private Upgrades upgrades;
    SzeneManager szeneManager;
    public GameObject OpenGatesShop;
    public GameObject HeartsShop;

    private readonly int[] UpgradeCost = { 50, 100, 150, 300, 500};

    // Texte

   [SerializeField] private TMP_Text GlobalGoldCopy;
   [SerializeField] private TMP_Text StaminaInfoText;
   [SerializeField] private TMP_Text GoldChanceInfoText;
   [SerializeField] private TMP_Text EndTimerDecreaseInfoText;
   [SerializeField] private TMP_Text holdDurationInfoText;
   [SerializeField] private TMP_Text GainALifeInfoText;
   [SerializeField] private TMP_Text addSpeedInfoText;
   [SerializeField] private TMP_Text OpenGatesInfoText;

   [SerializeField] private TMP_Text StaminaPriceInfoText;
   [SerializeField] private TMP_Text GoldChancePriceInfoText;
   [SerializeField] private TMP_Text EndTimerDecreasePriceInfoText;
   [SerializeField] private TMP_Text holdDurationPriceInfoText;
   [SerializeField] private TMP_Text GainALifePriceInfoText;
   [SerializeField] private TMP_Text addSpeedPriceInfoText;
   [SerializeField] private TMP_Text OpenGatesPriceInfoText;

   [SerializeField] private GameObject StaminaPrice;
   [SerializeField] private GameObject GoldChancePrice;
   [SerializeField] private GameObject EndTimerDecreasePrice;
   [SerializeField] private GameObject holdDurationPrice;
   [SerializeField] private GameObject addSpeedPrice;
   [SerializeField] private GameObject OpenGatesPrice;
   [SerializeField] private GameObject BuyALifePrice;
   

   public GameObject[] Stamina;
   public GameObject[] GoldChance;
   public GameObject[] EndTimerDecrease;
   public GameObject[] HoldTimer;
   public GameObject[] Speed;

    
    void Start()
    {
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        szeneManager = GameObject.Find("PlayerData").GetComponent<SzeneManager>();
        if (szeneManager.raid >= 3)
        {
         OpenGatesShop.SetActive(true);
         OpenGatesText();
        }
        if (upgrades.health < 3)
        {
         HeartsShop.SetActive(true);
         GainALifeText();
        }
        UpdateStamina();
        UpdateGoldChance();
        UpdateEndTimerDecrease();
        UpdateHoldTimer();
        UpdateSpeed();
    } 

    public void SetBorders()
    {
      UpdateStamina();
      UpdateGoldChance();
      UpdateEndTimerDecrease();
      UpdateHoldTimer();
      UpdateSpeed();
      StaminaText();
      GoldChanceText();
      EndTimerDecreaseText();
      AddSpeedText();
      HoldTimerText();
      GainALifeText();
      


    }

      public void Reset()
      {
         OpenGatesShop.SetActive(false);
         OpenGatesPrice.gameObject.SetActive(false);
         HeartsShop.SetActive(false);
      StaminaPrice.gameObject.SetActive(true);
      GoldChancePrice.gameObject.SetActive(true);
      EndTimerDecreasePrice.gameObject.SetActive(true);
      holdDurationPrice.gameObject.SetActive(true);
      addSpeedPrice.gameObject.SetActive(true);
      }

    public void SetGlobalGold()
    {
      GlobalGoldCopy.text = "" + upgrades.GlobalGold;
    }

     public void UpgradeStamina()
     {
        upgrades.UpgradeStamina();
        UpdateStamina();
     }
      public void UpgradeGoldChance()
     {
        upgrades.UpgradeGoldChance();
        UpdateGoldChance();
     }

      public void UpgradeEndTimerDecrease()
     {
        upgrades.UpgradeEndTimerDecrease();
        UpdateEndTimerDecrease();
     }
      public void UpgradeHoldTimer()
     {
        upgrades.UpgradeHoldTimer();
        UpdateHoldTimer();
     }

      public void GainALife()
     {
        upgrades.GainALife();
     }
      public void UpgradeAddSpeed()
     {
        upgrades.UpgradeAddSpeed();
        UpdateSpeed();
     }
      public void OpenGates()
     {
        upgrades.OpenGates();
     }

      public void UpdateStamina()
      {
         for (int i = 0; i < Stamina.Length; i++)
        {
            
            Stamina[i].gameObject.SetActive(i == upgrades.staminacurrentLevel );
        }
      }

      public void UpdateGoldChance()
      {
         for (int i = 0; i < GoldChance.Length; i++)
        {
            
            GoldChance[i].gameObject.SetActive(i == upgrades.goldChanceIncreasecurrentLevel);
        }
      }

      public void UpdateEndTimerDecrease()
      {
         for (int i = 0; i < EndTimerDecrease.Length; i++)
        {
            
            EndTimerDecrease[i].gameObject.SetActive(i == upgrades.endTimerDecreasecurrentLevel);
        }
      }

      public void UpdateHoldTimer()
      {
         for (int i = 0; i < HoldTimer.Length; i++)
        {
            
            HoldTimer[i].gameObject.SetActive(i == upgrades.holdDurationcurrentLevel);
        }
      }

      public void UpdateSpeed()
      {
         for (int i = 0; i < Speed.Length; i++)
        {
            
            Speed[i].gameObject.SetActive(i == upgrades.addSpeedcurrentLevel);
        }
      }











     public void StaminaText()
     {
      if (upgrades.staminacurrentLevel <= 4)
      {
        StaminaInfoText.text = $"Mehr Stamina lässt dich länger sprinten";
        StaminaPriceInfoText.text = "" + UpgradeCost[upgrades.staminacurrentLevel];
              }
      else
      {
         StaminaInfoText.text = $"Mehr Stamina lässt dich länger sprinten";
         StaminaPrice.gameObject.SetActive(false);
      }
     }

      public void GoldChanceText()
     {
      if (upgrades.goldChanceIncreasecurrentLevel <= 4)
      {
        GoldChanceInfoText.text = $"Höhere Chance wertvolle Schätze auszugraben";
        GoldChancePriceInfoText.text = "" + UpgradeCost[upgrades.goldChanceIncreasecurrentLevel];
      }
      else
      {
         GoldChanceInfoText.text = $"Höhere Chance wertvolle Schätze auszugraben";
         GoldChancePrice.gameObject.SetActive(false);
      }
      }

      public void EndTimerDecreaseText()
     {
      if (upgrades.endTimerDecreasecurrentLevel <= 4)
      {
        EndTimerDecreaseInfoText.text = $"Geringere Chance beim graben den Alarm auszulösen";
         EndTimerDecreasePriceInfoText.text = "" + UpgradeCost[upgrades.endTimerDecreasecurrentLevel];
      }
      else
      {
         EndTimerDecreaseInfoText.text = $"Geringere Chance beim graben den Alarm auszulösen";
         EndTimerDecreasePrice.gameObject.SetActive(false);
      }
      }
      public void HoldTimerText()
     {
      if (upgrades.holdDurationcurrentLevel <= 4)
      {
        holdDurationInfoText.text = $"Schnelleres Graben";
        holdDurationPriceInfoText.text = "" + UpgradeCost[upgrades.holdDurationcurrentLevel];
      }
      else
      {
         holdDurationInfoText.text = $"Schnelleres Graben";
         holdDurationPrice.gameObject.SetActive(false);
      }
      }

      public void AddSpeedText()
     {
      if (upgrades.addSpeedcurrentLevel <= 4)
      {
        addSpeedInfoText.text = $"Erhöhte Laufgeschwindigkeit";
        addSpeedPriceInfoText.text = "" + UpgradeCost[upgrades.addSpeedcurrentLevel];
      }
      else
      {
         addSpeedInfoText.text = $"Erhöhte Laufgeschwindigkeit";
         addSpeedPrice.gameObject.SetActive(false);

      }
      }

      public void GainALifeText()
     {
      if (upgrades.health <= 2)
      {
        GainALifeInfoText.text = $"Fülle ein fehlendes Herz wieder auf";
        BuyALifePrice.gameObject.SetActive(true);
        GainALifeInfoText.text = "" + upgrades.heartCost;
      }
      else
      {
         GainALifeInfoText.text = $"Fülle ein fehlendes Herz wieder auf";
         HeartsShop.SetActive(false);
         BuyALifePrice.gameObject.SetActive(false);
      }
      }

      public void OpenGatesText()
     {
      if (upgrades.openGates < 1)
      {
        OpenGatesInfoText.text = $"Öffnet nächste Runde alle Tore";
        OpenGatesPrice.gameObject.SetActive(true);
        addSpeedPriceInfoText.text = "" + upgrades.openGatescost;
      }
      else
      {
         OpenGatesInfoText.text = $"Alle Tore sind nächste Runde offen";
         OpenGatesPrice.gameObject.SetActive(false);
      }
      }
}
