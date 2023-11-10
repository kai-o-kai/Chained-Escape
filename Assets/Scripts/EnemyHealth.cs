using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable {
    public event Action OnDie;

    private float _health = 100f;
    private GameObject _hitByBulletFx;

    public void OnHitByBullet(Bullet bullet) {
        _health -= bullet.Damage;
        _hitByBulletFx = _hitByBulletFx ?? ReferenceManager.Instance.BulletHitEntityParticle;
        Destroy(Instantiate(_hitByBulletFx, bullet.transform.position, bullet.transform.rotation), 2f);
        Destroy(bullet.gameObject);
        if (_health <= 0f) {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
    public void OnHitByBaton(Baton baton) {
        return;
    }
}