using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable {
    public static event Action PlayerDie;
    public static event Action<PlayerHealth> PlayerHurt;

    public float Health { get; private set; } = 100f;

    public void DamageByBullet(float damageAmount, Bullet bullet) {
        Health -= damageAmount;
        PlayerHurt?.Invoke(this);
        if (Health <= 0f) {
            PlayerDie?.Invoke();
        }
    }
}
public interface IDamagable {
    public void DamageByBullet(float damageAmount, Bullet bullet);
}