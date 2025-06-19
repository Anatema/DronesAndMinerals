using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    [SerializeField]
    private float _globalSpeed = 1;
    public float GlobalSpeed => _globalSpeed;    

    [SerializeField][Tooltip("Amount per minute")]
    private float _resourceSpawnSpeed = 1;
    public float ResourceSpawnSpeed => _resourceSpawnSpeed;

    [SerializeField]
    private bool _isDrawPath = true;
    public bool IsDrawPath => _isDrawPath;

    [HideInInspector]
    public UnityEvent<float> OnGlobalSpeedChanged = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent<float> OnGlobalResourceSpawnSpeedChanged = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent<bool> OnIsDrawPathChanged = new UnityEvent<bool>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    public void SetGlobalSpeed(float value)
    {
        _globalSpeed = value;
        OnGlobalSpeedChanged?.Invoke(value);
    }
    public void SetGlobalResourceSpawnSpeed(float value)
    {
        _resourceSpawnSpeed = value;
        OnGlobalResourceSpawnSpeedChanged?.Invoke(value);
    }
    public void SetDrawPath(bool value)
    {
        _isDrawPath = value;
        OnIsDrawPathChanged?.Invoke(value);
    }
}
