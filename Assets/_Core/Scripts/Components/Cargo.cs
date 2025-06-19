using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Cargo : MonoBehaviour
{
    private Unit _unit;
    private void Start()
    {
        _unit = GetComponent<Unit>();
    }
    public void LoadCargo(int amount)
    {
        BattleManager.Instance?.AddResource(_unit.Side, amount);
    }
}
