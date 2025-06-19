using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class UnitState
{
    protected Unit _unit;
    [SerializeField]
    protected string _stateName;

    public UnitState(Unit unit)
    {
        _unit = unit;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
