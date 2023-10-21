using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable {
    public event Action OnDie;

    private float _health = 100f;
    private GameObject _hitByBulletFx;

    public void DamageByBullet(float damageAmount, Bullet bullet) {
        _health -= damageAmount;
        _hitByBulletFx = _hitByBulletFx ?? ReferenceManager.Instance.BulletHitEntityParticle;
        Destroy(Instantiate(_hitByBulletFx, bullet.transform.position, bullet.transform.rotation), 2f);
        Destroy(bullet.gameObject);
        if (_health <= 0f) {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}