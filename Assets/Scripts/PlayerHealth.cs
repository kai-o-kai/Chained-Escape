using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public static event Action PlayerDie;
    public static event Action<PlayerHealth> PlayerHurt;

    public float Health { get; private set; } = 100f;

    public void Damage(float damageAmount) {
        Health -= damageAmount;
        PlayerHurt?.Invoke(this);
        if (Health <= 0f) {
            PlayerDie?.Invoke();
        }
    }
}