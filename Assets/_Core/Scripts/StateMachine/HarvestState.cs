using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestState : UnitState
{
    private float _harvestCooldown;
    private float _harvestTimer;

    private Harvestable _harvestable;
    private Harvester _harvester;
    private bool _isHarvesting = false;
    public HarvestState(Unit unit, Harvestable harvestable, Harvester harvester): base(unit)
    {
        _stateName = "Harvest";
        _harvestable = harvestable;
        _harvester = harvester;
    }
    public override void Enter()    
    {
        _harvestCooldown = _harvester.HarvestCooldown;
        _harvestTimer = _harvestCooldown;
        _harvester.Harvest(_harvestable, true);
        _isHarvesting = true;
    }
    public override void Update()
    {
        _harvestTimer -= Time.deltaTime;

        if(_harvestTimer > 0)
        {
            return;
        }
        _harvestTimer = _harvestCooldown;
        
        _harvester.Harvest(_harvestable);
        if (!_harvestable || _harvestable.ResourceValue <= 0)
        {
            _unit.SetState(new SearchHarvestableState(_unit, _harvester));
        }
        if (_harvester.Harvested >= _harvester.HarvestCapasity)
        {
            if(!_harvester.SearchForCargo(out Cargo cargo))
            {
                _unit.SetState(new IdleState(_unit));
                return;
            }

            float targetRadius = 0;
            if (cargo.TryGetComponent(out Targetable targetable))
            {
                targetRadius = targetable.Radius;
            }
            if (!_unit.TryGetComponent(out Movement movement))
            {
                _unit.SetState(new IdleState(_unit));
                return;
            }
            _unit.SetState(new MoveState(_unit, movement, cargo.transform.position, new UnloadState(_unit, _harvester, cargo), targetRadius));
        }
    }
    
    public override void Exit()
    {
        if (_isHarvesting)
        {
            _harvestable?.StopHarvest();
        }
        _harvester.Stopharvesting();
        base.Exit();
    }
}
