using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Targetable))]
public abstract class Movement : MonoBehaviour
{
    protected Targetable _targetable;
    public Targetable Targetable=> _targetable;
    public abstract void MoveCommand(Vector3 target);
    public abstract void Move(Vector3 target);
    public abstract void Stop();
}
