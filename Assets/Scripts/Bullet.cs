using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    public float Damage { get; private set; }

    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Shoot(float speed, int layer, float damage, float innacuracyDegrees) {
        float randomAngle = Random.Range(-innacuracyDegrees, innacuracyDegrees);
        float newAngle = _rigidbody.rotation + randomAngle;
        transform.eulerAngles = new Vector3(0f, 0f, newAngle);
        _rigidbody.velocity = transform.up * speed;
        gameObject.layer = layer;
        Damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Tool Collider")) { return; }
        
        if (other.TryGetComponent<IDamagable>(out var damagable)) {
            damagable.OnHitByBullet(this);
        } else {
            // Hit a non damagable object
            // TODO : Bullet hit wall fx
            Destroy(gameObject);
        }
    }
}