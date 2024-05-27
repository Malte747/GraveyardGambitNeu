using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GetChased : MonoBehaviour
{
    private Vignette _vignette;
    private Coroutine _currentCoroutine;
    private bool _isChased = false;

    [SerializeField] private VolumeProfile volumeProfile;

    private void Start()
    {
        if (volumeProfile == null)
        {
            Debug.LogError("VolumeProfile is not assigned.");
            return;
        }

        if (!volumeProfile.TryGet(out _vignette))
        {
            Debug.LogError("Vignette component not found in the assigned volume profile.");
        }
        _vignette.intensity.value = 0f;
    }

    public void Chase()
    {
        if (!_isChased)
        {
            _isChased = true;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(ChangeVignetteIntensity(0.75f, 3f));
        }
    }

    public void DontChase()
    {

        if (_isChased)
        {
            _isChased = false;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(ChangeVignetteIntensity(0f, 2f));
        }
    }

    private IEnumerator ChangeVignetteIntensity(float targetIntensity, float duration)
    {
        if (_vignette == null)
        {
            Debug.LogError("Vignette component is null.");
            yield break;
        }

        float startIntensity = _vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);
            yield return null;
        }

        _vignette.intensity.value = targetIntensity;
        _currentCoroutine = null;
    }
}
