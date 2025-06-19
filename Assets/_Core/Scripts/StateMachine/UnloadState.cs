using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadState : UnitState
{
    private Harvester _harvester;
    private Cargo _cargo;
    public UnloadState(Unit unit, Harvester harvester, Cargo cargo): base(unit)
    {
        _harvester = harvester;
        _cargo = cargo;
    }
    public override void Enter()
    {

    }
    public override void Update()
    {
        if(_cargo == null)
        {
            _unit.SetState(new IdleState(_unit));
            return;
        }
        _harvester.Unload(_cargo);
        _unit.SetState(new WaitState(_unit, 0.5f, new SearchHarvestableState(_unit, _harvester)));

    }
}
