using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private Upgrades upgrades;

    private readonly int[] UpgradeCost = { 100, 200, 400, 800, 1600};

    // Texte

    [SerializeField] private TMP_Text StaminaInfoText;
   [SerializeField] private TMP_Text GoldChanceInfoText;
   [SerializeField] private TMP_Text EndTimerDecreaseInfoText;
   [SerializeField] private TMP_Text holdDurationInfoText;
    
    void Start()
    {
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
    }

    

     public void UpgradeStamina()
     {
        upgrades.UpgradeStamina();
     }
      public void UpgradeGoldChance()
     {
        upgrades.UpgradeGoldChance();
     }

      public void UpgradeEndTimerDecrease()
     {
        upgrades.UpgradeEndTimerDecrease();
     }
      public void UpgradeHoldTimer()
     {
        upgrades.UpgradeHoldTimer();
     }




     public void StaminaText()
     {
      if (upgrades.staminacurrentLevel <= 4)
      {
        StaminaInfoText.text = $"Mehr Stamina lässt dich länger sprinten. Level: {upgrades.staminacurrentLevel} Kosten für das nächste Upgrade: {UpgradeCost[upgrades.staminacurrentLevel]}";
              }
      else
      {
         StaminaInfoText.text = $"Höhere Chance auf Wertvollere Beute. Level: {upgrades.staminacurrentLevel}";
      }
     }

      public void GoldChanceText()
     {
      if (upgrades.goldChanceIncreasecurrentLevel <= 4)
      {
        GoldChanceInfoText.text = $"Höhere Chance auf Wertvollere Beute. Level: {upgrades.goldChanceIncreasecurrentLevel} Kosten für das nächste Upgrade: {UpgradeCost[upgrades.goldChanceIncreasecurrentLevel]}";
      }
      else
      {
         GoldChanceInfoText.text = $"Höhere Chance auf Wertvollere Beute. Level: {upgrades.goldChanceIncreasecurrentLevel}";
      }
      }

      public void EndTimerDecreaseText()
     {
      if (upgrades.endTimerDecreasecurrentLevel <= 4)
      {
        EndTimerDecreaseInfoText.text = $"Geringere wahrscheinlihckeit bei Graböffnung entdeckt zu werden. Level: {upgrades.endTimerDecreasecurrentLevel} Kosten für das nächste Upgrade: {UpgradeCost[upgrades.endTimerDecreasecurrentLevel]}";
      }
      else
      {
         EndTimerDecreaseInfoText.text = $"Geringere wahrscheinlihckeit bei Graböffnung entdeckt zu werden. Level: {upgrades.endTimerDecreasecurrentLevel}";
      }
      }
      public void HoldTimerText()
     {
      if (upgrades.holdDurationcurrentLevel <= 4)
      {
        holdDurationInfoText.text = $"Schneller Graben. Level: {upgrades.holdDurationcurrentLevel} Kosten für das nächste Upgrade: {UpgradeCost[upgrades.holdDurationcurrentLevel]}";
      }
      else
      {
         holdDurationInfoText.text = $"Schneller Graben. Level: {upgrades.holdDurationcurrentLevel}";
      }
      }
}