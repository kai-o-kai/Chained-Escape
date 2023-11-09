using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable {
    public static event Action PlayerDie;
    public static event Action<PlayerHealth> PlayerHurt;

    public float Health { get; private set; } = 100f;

    private GameObject _hitByBulletFx;

    public void OnHitByBullet(Bullet bullet) {
        Health -= bullet.Damage;
        PlayerHurt?.Invoke(this);
        _hitByBulletFx = _hitByBulletFx ?? ReferenceManager.Instance.BulletHitEntityParticle;
        Destroy(Instantiate(_hitByBulletFx, bullet.transform.position, bullet.transform.rotation), 2f);
        Destroy(bullet.gameObject);
        if (Health <= 0f) {
            PlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
public interface IDamagable {
    public void OnHitByBullet(Bullet bullet);
}