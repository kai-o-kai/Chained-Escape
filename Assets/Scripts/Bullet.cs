using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private float _damage;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Shoot(float speed, int layer, float damage) {
        _rigidbody.velocity = transform.up * speed;
        gameObject.layer = layer;
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IDamagable>(out var damagable)) {
            damagable.DamageByBullet(_damage, this);
        } else {
            // Hit a non damagable object
            // TODO : Bullet hit wall fx
            Destroy(gameObject);
        }
    }
}