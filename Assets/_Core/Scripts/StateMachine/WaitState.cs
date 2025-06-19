using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : UnitState
{
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private UnitState _nextState;
    public WaitState(Unit unit, float waitingTime, UnitState state) : base(unit)
    {
        _stateName = "Wait";
        _cooldown = waitingTime;
        _nextState = state;
    }
    public override void Update()
    {
        _cooldown -= Time.deltaTime;
        if (_cooldown <= 0)
        {
            _unit.SetState(_nextState);
        }
    }
}
