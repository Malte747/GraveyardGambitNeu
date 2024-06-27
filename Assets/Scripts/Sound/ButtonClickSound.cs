using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonBuy;
    






    public void ButtonHoverSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonHover, transform, 0.3f);
    }

        public void ClickSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClick, transform, 0.3f);
    }

        public void BuySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonBuy, transform, 0.5f);
    }


}
