using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Harvester>()?.StartHarvest();
    }
    
}
