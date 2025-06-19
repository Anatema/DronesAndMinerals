using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeReference]
    private UnitState _currentState;
    [SerializeField]
    private byte _side;
    public byte Side => _side;
    private void Start()
    {
        ObjectsManager.Instance?.AddUnit(this);
    }
    void Update()
    {
        _currentState?.Update();
    }
    public void SetState(UnitState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

}
