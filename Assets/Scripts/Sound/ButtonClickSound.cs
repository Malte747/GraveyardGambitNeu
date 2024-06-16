using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioClip buttonClick;






    public void ButtonHoverSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonHover, transform, 1f);
    }

        public void ClickSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClick, transform, 1f);
    }
}
