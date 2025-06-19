using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MapSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _droneSpeedSlider;
    [SerializeField] private Slider _droneNumbersSlider;
    [SerializeField] private TMP_InputField _resourcesSpeedInputField;
    [SerializeField] private Toggle _showPathToggle;
    private GameSettings _settings;
    public void Awake()
    {
        if (!GameSettings.Instance)
        {
            Debug.LogError("No game setting instance");
            return;
        }
        _settings = GameSettings.Instance;
    }
    public void Start()
    {
        _droneSpeedSlider.value = _settings.GlobalSpeed;
        _resourcesSpeedInputField.text = _settings.ResourceSpawnSpeed.ToString();
        _showPathToggle.isOn = _settings.IsDrawPath;

        _droneSpeedSlider.onValueChanged.AddListener(ChangeDroneSpeed);
        _resourcesSpeedInputField.onEndEdit.AddListener(ChangeResourceSpawnSpeed);
        _showPathToggle.onValueChanged.AddListener(OnShowPathChanged);
        _droneNumbersSlider.onValueChanged.AddListener(OnDroneAmountChanged);



    }

    private void OnDroneAmountChanged(float value)
    {
        BattleManager.Instance?.ChangeNumberOfDrones((int)value);
    }

    private void OnShowPathChanged(bool value)
    {
        _settings?.SetDrawPath(value);
    }

    private void ChangeDroneSpeed(float value)
    {
        _settings?.SetGlobalSpeed(value);
    }
    private void ChangeResourceSpawnSpeed(string value)
    {
        if (float.TryParse(value, out float result))
        {
            _settings?.SetGlobalResourceSpawnSpeed(result);
        }
    }
    public void OnDestroy()
    {
        _droneSpeedSlider.onValueChanged.RemoveListener(ChangeDroneSpeed);
        _resourcesSpeedInputField.onEndEdit.RemoveListener(ChangeResourceSpawnSpeed);
        _showPathToggle.onValueChanged.RemoveListener(OnShowPathChanged);
    }
}
