using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable 
{
    void Interact();
    void Visible();
    void InVisible();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public float holdDuration = 3f;
    
    private Slider openGrave;
    public LayerMask ignoreLayers;

    [SerializeField] public bool seeTarget = false;
    [SerializeField] private float holdTimer = 0f;

   void Start()
   {
    openGrave = GameObject.Find("ProgressGrave").GetComponent<Slider>();
    openGrave.gameObject.SetActive(false);
   }

  
   void Update()
{
    openGrave.value = holdTimer;
    Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
   
    if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ~ignoreLayers))
    {
        if (hitInfo.collider.CompareTag("Grave") && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
        {
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

                if (holdTimer >= holdDuration)
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

