using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Harvestable _harvestablePrefab;
    [SerializeField] private Rect _spawnArea;
    [SerializeField] private float _spawnSpeed;
    private float _nextSpawnTime;
    private const float _minute = 60;

    public void Start()
    {
        if (!GameSettings.Instance)
        {
            return;
        }
        _spawnSpeed = GameSettings.Instance.ResourceSpawnSpeed;
        GameSettings.Instance.OnGlobalResourceSpawnSpeedChanged.AddListener(SetSpawnSpeed);
    }
    private void SetSpawnSpeed(float value)
    {
        _spawnSpeed = value;

        CalculateNextSpawnTime();
    }
    private void OnDestroy()
    {
        GameSettings.Instance.OnGlobalResourceSpawnSpeedChanged.RemoveListener(SetSpawnSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        if (_nextSpawnTime <= Time.time)
        {
            CalculateNextSpawnTime();
            SpawnHarvestable();
        }
    }
    private void CalculateNextSpawnTime()
    {
        _nextSpawnTime = Time.time + _minute / _spawnSpeed; 
    }
    private void SpawnHarvestable()
    {
        float horizontal = _spawnArea.x + Random.Range(-_spawnArea.width, _spawnArea.width);
        float vertical = _spawnArea.y + Random.Range(-_spawnArea.height, _spawnArea.height);
        Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
        Harvestable harvestable = Instantiate(_harvestablePrefab, new Vector3(horizontal, 0.5f, vertical), Quaternion.Euler(rotation), transform);
        ObjectsManager.Instance?.AddHarvestable(harvestable);
    }
}
