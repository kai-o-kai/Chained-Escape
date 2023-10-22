using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable {
    public static event Action PlayerDie;
    public static event Action<PlayerHealth> PlayerHurt;

    public float Health { get; private set; } = 100f;

    public void OnHitByBullet(Bullet bullet) {
        Health -= bullet.Damage;
        PlayerHurt?.Invoke(this);
        if (Health <= 0f) {
            PlayerDie?.Invoke();
        }
    }
}
public interface IDamagable {
    public void OnHitByBullet(Bullet bullet);
}