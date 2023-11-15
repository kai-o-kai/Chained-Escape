using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Medkit : MonoBehaviour {
    [SerializeField] private float _healAmount;
    
    private AudioSource _source;
    private AudioClip _pickupSound;

    private void Awake() {
        _source = GetComponent<AudioSource>();
    }
    private void Start() {
        _pickupSound = ReferenceManager.Instance.MedkitPickup;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<PlayerHealth>(out var playerHealth)) {
            playerHealth.Heal(_healAmount);
            _source.PlayOneShot(_pickupSound);
            Destroy(gameObject);
        }
    }
}