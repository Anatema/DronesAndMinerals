using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    private Harvestable _harvestablePrefab;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnHarvestable();
        }
    }
    private void SpawnHarvestable()
    {
        Instantiate(_harvestablePrefab, transform);
    }
}
