using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _minDistance = 3f;

    void Start()
    {
        PlaceAgentOnNavMesh(transform.position);
        MoveToRandomPoint();
    }

    void Update()
    {
        if (HasReachedDestination())
        {
            MoveToRandomPoint();
        }
    }

    public void MoveTo(Vector3 position)
        => _agent.SetDestination(GetValidSpawnPosition(position));

    private void MoveToRandomPoint()
    {
        int random = Random.Range(0, _wayPoints.Length);
        MoveTo(_wayPoints[random].position);
    }

    private bool HasReachedDestination()
    {
        if (!_agent.enabled || !_agent.isOnNavMesh || _agent.pathPending) 
            return false;

        return _agent.remainingDistance <= _agent.stoppingDistance + _minDistance;
    }

    private Vector3 GetValidSpawnPosition(Vector3 originalPosition, float maxDistance = 100f)
    { 
        return NavMesh.SamplePosition(originalPosition, out NavMeshHit hit, maxDistance, NavMesh.AllAreas) 
            ? hit.position 
            : originalPosition;
    }
    
    private void PlaceAgentOnNavMesh(Vector3 position)
    {
        Vector3 validPosition = _agent.isOnNavMesh ? position : GetValidSpawnPosition(position);
        _agent.Warp(validPosition);
    }
}
