using Unity.VisualScripting;
using UnityEngine;

public class BatonEnemy : Enemy {
    private const float MINATTACKDISTANCE = 1.5f;
    private const float TURNSPEED = 100f;

    [SerializeField]
    private Baton _baton;

    private float _attackTimer;
    private bool _readyToAttack => _attackTimer <= 0f;

    protected override void Update() {
        base.Update();
        if (CanSeePlayer()) {
            TurnToPlayer(TURNSPEED, 0f);
            Destination = Player.position;
            if (Vector2.Distance(Player.position, transform.position) <= MINATTACKDISTANCE) {
                if (_readyToAttack) {
                    Attack();
                    _attackTimer = _baton.SecondsBetweenHits;
                } else {
                    _attackTimer -= Time.deltaTime;
                }
            } else {
                _attackTimer = 0f;
            }
        } else {            
            Destination = RememberedPlayerPosition;
        }
        void Attack() {
            if (Player.TryGetComponent<IDamagable>(out var damagable)) {
                damagable.OnHitByBaton(_baton);
            }
        }
    }
}