using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath), typeof(Seeker))]
public abstract class Enemy : MonoBehaviour {
    protected Transform Player { get; private set; }
    protected float Speed { get => _path.maxSpeed; set => _path.maxSpeed = value; }
    protected Vector2 Destination { get => _path.destination; set => _path.destination = value; }
    protected Vector2 RememberedPlayerPosition { get; private set; }
    protected bool DestinationReached => _path.remainingDistance < 0.1f;
    protected bool EnemyIsEnabled = true;

    [SerializeField]
    private LayerMask _sightToPlayerMask;
    private AIPath _path;

    protected virtual void Awake() {
        _path = GetComponent<AIPath>();
    }

    protected virtual void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHealth.PlayerDie += OnPlayerDie;
        RememberedPlayerPosition = transform.position;
    }
    protected virtual void Update() {
        SetRememberedPlayerPos();

        void SetRememberedPlayerPos() {
            if (!EnemyIsEnabled) { 
                RememberedPlayerPosition = transform.position;    
                return; 
            }

            if (CanSeePlayer()) {
                RememberedPlayerPosition = Player.position;
            }
        }
    }
    protected void TurnToPlayer(float turnSpeed, float innacuracy) {
        float modifier = Random.Range(-innacuracy, innacuracy);
        Quaternion targetRot = Quaternion.Euler(0f, 0f, GetAngleToPlayer() + modifier);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
    }
    private float GetAngleToPlayer() => Utilities.GetAngleToPoint(Player.position, transform.position) + 90f;
    protected bool TurnedToPlayer() {
        float targetAngle = GetAngleToPlayer();
        float difference = Mathf.Abs(targetAngle - transform.eulerAngles.z);
        if (difference > 360f) {
            difference = difference % 360f;
        }
        return difference < 5f;
    }
    protected bool CanSeePlayer() {
        if (!EnemyIsEnabled)  { return false; }

        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, Player.position, _sightToPlayerMask);
        if (hits.Length == 1) { 
            return false; 
        }

        RaycastHit2D hitData = hits[1]; // 0 is self, so 1 should be player. Physics2D.queriesStartInColliders didnt work to fix this.
        if (hitData.collider is null) {
            Debug.LogError("Enemy CanSeePlayer call raycast returned with no results.", this);
            return false;
        }
        return hitData.transform == Player.transform;
    }
    private void OnDrawGizmos() {
        if (!Application.isPlaying) { return; }
        if (!EnemyIsEnabled) { return; }

        if (CanSeePlayer()) {
            Gizmos.color = Color.green;
        } else {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(transform.position, Player.position);
    }
    private void OnPlayerDie() => EnemyIsEnabled = false;
    private void OnDestroy() {
        PlayerHealth.PlayerDie -= OnPlayerDie;
    }
}