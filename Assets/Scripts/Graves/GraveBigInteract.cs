using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveBigInteract : MonoBehaviour, IInteractable
{
         LevelGold levelGold;
         Interactor interactor;
        public float getGoldPercent = 79f;
        public Canvas canvastext;
        public GameObject particlesystem;
        public GameObject Lid;
        private BoxCollider boxCollider;
        private Animator animator;
        Upgrades upgrades;
         [SerializeField] private string LidTrigger = "LidTrigger";
        

        void Start()
        {
            levelGold = GameObject.Find("GM").GetComponent<LevelGold>();
            interactor = GameObject.Find("Cam_holder").GetComponent<Interactor>();
            canvastext.gameObject.SetActive(false);
            upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
            boxCollider = GetComponent<BoxCollider>();
            animator = GetComponent<Animator>();
            
        }

        void Update()
        {
            if (interactor.seeTarget == false)
            {
                canvastext.gameObject.SetActive(false);
            }
           
        }
    	public void Interact()
        {
            if (levelGold != null)
            {
                    
                            
                            StartCoroutine(ActivateParticleSystemAfterDelay(2f));
                            boxCollider.enabled = false;
                            animator.SetTrigger(LidTrigger);
                            Debug.Log("Jackpot");
                        
                
            }
        }

        private IEnumerator ActivateParticleSystemAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        particlesystem.gameObject.SetActive(true);
        levelGold.IncreaseGold(150);
    }

        public void Visible()
        {
             canvastext.gameObject.SetActive(true);
        }

        public void InVisible()
        {
             canvastext.gameObject.SetActive(false);
        }
}
