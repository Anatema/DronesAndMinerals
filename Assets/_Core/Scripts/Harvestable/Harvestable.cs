using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harvestable : MonoBehaviour
{
    public bool IsHarvested => _harvestetBy != null;

    [SerializeField]
    protected int _resourceValue;
    public int ResourceValue => _resourceValue;

    [SerializeField]
    protected Unit _harvestetBy;
    public Unit HarvestedBy => _harvestetBy;

    public abstract void Harvest(Unit harvester, int harvestAmount, out int harvestedAmount);


    public abstract void StopHarvest();
  

}
