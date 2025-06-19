using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneMovement : Movement
{
    private NavMeshAgent _agent;
    private Vector3 _target;
    [SerializeField]
    private float _rotationSpeed;
    private Unit _unit;

    private float _gloablSpeedModifier;
    [SerializeField]
    private float _speed;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetable = GetComponent<Targetable>();
        _unit = GetComponent<Unit>();
    }
    private void Start()
    {
        Stop();
        _speed = _agent.speed;
        if (!GameSettings.Instance)
        {
            return;
        }
        OnGlobalSpeedChanged(GameSettings.Instance.GlobalSpeed);
        GameSettings.Instance?.OnGlobalSpeedChanged.AddListener(OnGlobalSpeedChanged);
        //StopRange = _agent.stoppingDistance;
    }
    private void OnGlobalSpeedChanged(float value)
    {
        _agent.speed = _speed * value;
    }
    private void OnEnable()
    {
        if (_agent != null)
            _agent.SetDestination(_target);
    }
    private void OnDestroy()
    {
        GameSettings.Instance?.OnGlobalSpeedChanged.RemoveListener(OnGlobalSpeedChanged);
    }
    public override void Move(Vector3 target)
    {
        _target = target;
        _agent.isStopped = false;
        _agent.SetDestination(target);
    }
    public void Update()
    {
        if (_agent.isStopped)
        {
            return;
        }
        Rotate();


    }
    private void Rotate()
    {
        Vector3 direction = _target - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
    public override void Stop()
    {
        _agent.isStopped = true;
    }

    public override void MoveCommand(Vector3 target)
    {
        _unit.SetState(new MoveState(_unit, this, target, new IdleState(_unit)));
    }
}
