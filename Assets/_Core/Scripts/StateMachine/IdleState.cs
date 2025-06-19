using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitState
{
    public IdleState(Unit unit) : base(unit)
    {
        _stateName = "Idle";
    }
}
