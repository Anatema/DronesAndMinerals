using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : UnitState
{
    private Movement _movement;
    private Vector3 _target;
    private UnitState _nextState;
    public float Distance;
    private float _targetRadius;
    public MoveState(Unit unit, Movement movement, Vector3 target, UnitState nextState, float targetRadius = 0) : base(unit)
    {
        _stateName = "Move";
        _movement = movement;
        _target = target;
        _nextState = nextState;
        _targetRadius = targetRadius;
    }
    public override void Enter()
    {
        _movement.Move(_target);
    }
    public override void Update()
    {
        float distance = Vector3.Distance(_movement.transform.position , _target);
        Distance = distance;
        if (distance <= _movement.Targetable.Radius + _targetRadius)
        {
            
            _unit.SetState(_nextState);
        }
    }
    public override void Exit()
    {
        _movement.Stop();
    }
}
