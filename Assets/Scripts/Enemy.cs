using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath), typeof(Seeker))]
public abstract class Enemy : MonoBehaviour {
    protected Transform Player { get; private set; }
    protected float Speed { get => _path.maxSpeed; set => _path.maxSpeed = value; }
    protected Vector2 Destination { get => _path.destination; set => _path.destination = value; }
    protected bool destinationReached => _path.reachedDestination;

    private AIPath _path;

    protected virtual void Awake() {
        _path = GetComponent<AIPath>();
    }

    protected virtual void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected bool CanSeePlayer() {
        RaycastHit2D hitData = Physics2D.Raycast(transform.position, Player.position);
        if (hitData.collider is null) {
            Debug.LogError("Enemy CanSeePlayer call raycast returned with no results.", this);
            return false;
        }
        return hitData.transform == Player.transform;
    }
}