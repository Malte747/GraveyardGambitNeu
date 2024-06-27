using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChaseMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private const float fadeDuration = 1.0f;
    private const float initialVolume = 0.6f;

    [SerializeField] private bool canFadeOut = true;
    [SerializeField] private bool canSetVolume = true;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
    }

    public void FadeOutAndDisable()
    {
        if (canFadeOut && canSetVolume)
        {
            canFadeOut = false;
            canSetVolume = false;
            StartCoroutine(FadeOutAndDisableCoroutine());
        }
    }

    private IEnumerator FadeOutAndDisableCoroutine()
    {
        float startVolume = audioSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0;
        canSetVolume = true;
        gameObject.SetActive(false);
        
    }

    public void SetVolumeAndEnable()
    {
        if (canSetVolume && !canFadeOut)
        {
            canSetVolume = true;
            canFadeOut = true;
            gameObject.SetActive(true);
            audioSource.volume = initialVolume;
        }
    }
}
