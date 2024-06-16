using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicesManager : MonoBehaviour
{
    public static VoicesManager instance;

    [SerializeField] private AudioSource VoicesObject;

    private HashSet<AudioClip> playingClips = new HashSet<AudioClip>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

        public void PlayVoicesClipOneTime(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (playingClips.Contains(audioClip))
        {
            // If the clip is already playing, return and do not play it again.
            return;
        }

        AudioSource audioSource = Instantiate(VoicesObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;

        playingClips.Add(audioClip);

        StartCoroutine(RemoveClipFromPlaying(audioClip, clipLength));
        Destroy(audioSource.gameObject, clipLength + 1);
    }

    private IEnumerator RemoveClipFromPlaying(AudioClip audioClip, float delay)
    {
        yield return new WaitForSeconds(delay);
        playingClips.Remove(audioClip);
    }


    public void PlayVoicesClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(VoicesObject, spawnTransform.position, Quaternion.identity);

        

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength + 1);

    }

        public void PlayRandomVoicesFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(VoicesObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength + 1);

    }
}

