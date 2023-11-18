using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable {
    public static event Action PlayerDie;
    public static event Action<PlayerHealth> PlayerHealthChanged;

    public float Health { get; private set; } = 100f;

    private GameObject _hitByBulletFx;

    private void Start() {
        PlayerDie += () => {
            Invoke(nameof(ResetLevel), 1f);
        };
    }
    private void ResetLevel() {
        LevelTransitionManager.Instance.TransitionToScene(LevelTransitionManager.Instance.CurrentSceneIndex, false);
    }

    public void OnHitByBullet(Bullet bullet) {
        Health -= bullet.Damage;
        PlayerHealthChanged?.Invoke(this);
        _hitByBulletFx = _hitByBulletFx ?? ReferenceManager.Instance.BulletHitEntityParticle;
        Destroy(Instantiate(_hitByBulletFx, bullet.transform.position, bullet.transform.rotation), 2f);
        Destroy(bullet.gameObject);
        if (Health <= 0f) {
            PlayerDie?.Invoke();
            Destroy(gameObject);
        }
    }
    public void OnHitByBaton(Baton baton) {
        Health -= baton.HitDamage;
        PlayerHealthChanged?.Invoke(this);
    }
    public void Heal(float healAmount) {
        Health += healAmount;
    }
}
public interface IDamagable {
    public void OnHitByBullet(Bullet bullet);
    public void OnHitByBaton(Baton baton);
}