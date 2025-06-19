using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class PathVisualisation : MonoBehaviour
{
    private NavMeshAgent _agent;
    private LineRenderer _lineRenderer;
    private bool _isDrawPath;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        if (!GameSettings.Instance)
        {
            return;
        }
        _isDrawPath = GameSettings.Instance.IsDrawPath;
        GameSettings.Instance.OnIsDrawPathChanged.AddListener(SetDrawPath);
    }
    private void SetDrawPath(bool value)
    {
        _isDrawPath = GameSettings.Instance.IsDrawPath;
        _lineRenderer.enabled = value;

    }
    // Update is called once per frame
    void Update()
    {
        DrawPath();
    }
    private void DrawPath()
    {
        if (!_isDrawPath)
        {
            return;
        }
        if (_agent.isStopped)
        {
            return;
        }
        if (!_agent.hasPath)
        {
            return;
        }

        _lineRenderer.positionCount = _agent.path.corners.Length;
        _lineRenderer.SetPositions(_agent.path.corners);
    }
    private void OnDestroy()
    {

        GameSettings.Instance.OnIsDrawPathChanged.RemoveListener(SetDrawPath);
    }
}
