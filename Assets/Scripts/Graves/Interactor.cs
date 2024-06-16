using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

interface IInteractable 
{
    void Interact();
    void Visible();
    void InVisible();
}

public class Interactor : MonoBehaviour
{
    Upgrades upgrades;
    public Transform InteractorSource;
    public float InteractRange;
    [SerializeField] private float localHoldTimer;
    [SerializeField] private float floattimer;
    
    private Slider openGrave;
    public LayerMask ignoreLayers;

    [SerializeField] public bool seeTarget = false;
    [SerializeField] private float holdTimer = 0f;

    [SerializeField] private AudioClip Digging; 

   void Start()
   {
    openGrave = GameObject.Find("ProgressGrave").GetComponent<Slider>();
    upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
    openGrave.gameObject.SetActive(false);
    localHoldTimer = upgrades.holdDuration;
    floattimer = localHoldTimer/10;
    openGrave.maxValue = floattimer;
    
   }

  
   void Update()
{
    openGrave.value = holdTimer;
    Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
   
    if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ~ignoreLayers))
    {
        if (hitInfo.collider.CompareTag("Grave") && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
        {
            Debug.DrawRay(r.origin, r.direction * InteractRange, Color.yellow);
            interactObj.Visible();
            seeTarget = true;

            if (Input.GetMouseButtonDown(0))
            {
                holdTimer = 0f;
            }

            if (Input.GetMouseButton(0) && seeTarget)
            {
                holdTimer += Time.deltaTime;
                openGrave.gameObject.SetActive(true);
                interactObj.InVisible();
                if (hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("Grave"))
                {
                    SoundFXManager.instance.PlaySoundFXClipOneTime(Digging, transform, 0.5f);
                }

                if (holdTimer >= floattimer)
                {
                    InteractAfterDelay();
                    seeTarget = false;
                    holdTimer = 0f;
                }
            }
            else
            {
                openGrave.gameObject.SetActive(false);
                holdTimer = 0f;
                
            }
        }
        else
        {
            seeTarget = false;
            holdTimer = 0f;
            openGrave.gameObject.SetActive(false);
            
        }

    }
    else
    {
            seeTarget = false;
            holdTimer = 0f;
            openGrave.gameObject.SetActive(false);
            
    }
}





    private void InteractAfterDelay()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ~ignoreLayers))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }
}

