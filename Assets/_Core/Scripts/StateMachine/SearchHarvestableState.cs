using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchHarvestableState : UnitState
{
    [SerializeField]
    private float _cooldown;

    [SerializeField]
    private float _searchRefreshTime = 1f;
    private Harvester _harvester;
    public SearchHarvestableState(Unit unit, Harvester harvester) : base(unit)
    {
        _stateName = "Search";
        _harvester = harvester;
    }
    public override void Enter()
    {
        Search();
    }
    private void Search()
    {
        UnitState state;
        if (!_harvester.SearchForHarvestable(out Harvestable harvestable))
        {
            state = new WaitState(_unit, _searchRefreshTime, new SearchHarvestableState(_unit, _harvester));
            _unit.SetState(state);
            return;
        }

        float targetRadius = 0;
        if (harvestable.TryGetComponent(out Targetable targetable))
        {
            targetRadius = targetable.Radius;
        }
        if(!_unit.TryGetComponent(out Movement movement))
        {
            _unit.SetState(new IdleState(_unit));
            return;
        }
        state = new MoveState(_unit, movement, harvestable.transform.position, new HarvestState(_unit, harvestable, _harvester), targetRadius);
        _unit.SetState(state);
    }
    
}
