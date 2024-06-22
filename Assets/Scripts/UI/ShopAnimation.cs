using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private string boolParameterName = "isRunning"; // Name of the Bool parameter in the Animator
    private Upgrades upgrades;
    void Start()
    {
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        if (upgrades.raidcount == 2)
        {
         animator.SetBool(boolParameterName, true);
        }
    }


    public void DeactivateBool()
    {
        if (animator != null)
        {
            animator.SetBool(boolParameterName, false);
        }
    }
}
