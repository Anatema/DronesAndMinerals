using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : Harvestable
{
    public override void Harvest(Unit harvester, int harvestAmount, out int harvestedAmount)
    {
        _harvestetBy = harvester;
        if (harvestAmount <= 0)
        {
            harvestedAmount = 0;
            return;
        }
        if(_resourceValue > harvestAmount)
        {
            harvestedAmount = harvestAmount;
            _resourceValue -= harvestAmount;
            return;
        }
        else
        {
            harvestedAmount = _resourceValue;
            _resourceValue = 0;
            ObjectsManager.Instance?.RemoveHarvestable(this);
            Destroy(gameObject);
            
        }
    }

    public override void StopHarvest()
    {
        _harvestetBy = null;
    }
}
