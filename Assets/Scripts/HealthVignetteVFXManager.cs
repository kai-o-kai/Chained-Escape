using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class HealthVignetteVFXManager : MonoBehaviour {
    private const float MAXVIGNETTEINTENSITY = 0.65f;
    private const float MINVIGNETTEINTENSITY = 0.45f;
    private const float SETTLESPEED = 1f;

    [SerializeField] private PlayerHealth _playerHPManger;

    private Vignette _vignette;
    private float _lastPlayerHealth;
    private float _playerMaxHP;

    private void Awake() {
        VolumeProfile volume = GetComponent<Volume>().profile;
        if (!volume.TryGet<Vignette>(out var vignette)) {
            Debug.LogError("No vignette on post processing stack! HealthVignetteVFX will not work.");
            enabled = false;
        } else {
            _vignette = vignette;
        }
        _lastPlayerHealth = _playerHPManger.Health;
        _playerMaxHP = _playerHPManger.Health;
        PlayerHealth.PlayerHealthChanged += RecalculateVignette;
    }
    private void OnDestroy() {
        PlayerHealth.PlayerHealthChanged -= RecalculateVignette;
    }
    private void RecalculateVignette(PlayerHealth hpManager) {
        float difference = _lastPlayerHealth - hpManager.Health;
        StartCoroutine(C_VignetteAnimation(difference, hpManager.Health));
        _lastPlayerHealth = hpManager.Health;
    }
    private IEnumerator C_VignetteAnimation(float difference, float newPlayerHealth) {
        float newValue = Mathf.Lerp(MINVIGNETTEINTENSITY, MAXVIGNETTEINTENSITY, difference / _playerMaxHP);
        newValue += _vignette.intensity.value;
        newValue = Mathf.Clamp(newValue, MINVIGNETTEINTENSITY, MAXVIGNETTEINTENSITY);
        _vignette.intensity.Override(newValue);
        _vignette.color.Override(Color.red);
        float settledValue = Mathf.Lerp(MAXVIGNETTEINTENSITY, MINVIGNETTEINTENSITY, newPlayerHealth / _playerMaxHP);
        Debug.Log($"Health Vignette Recalculating, jump value {newValue}, settled value {settledValue}");
        while (_vignette.intensity.value != settledValue) {
            _vignette.intensity.Override(Mathf.Lerp(_vignette.intensity.value, settledValue, Time.deltaTime * SETTLESPEED));
            yield return null;
        }
    }
}