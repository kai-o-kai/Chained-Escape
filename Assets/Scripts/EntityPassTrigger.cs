using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EntityPassTrigger : MonoBehaviour {
    public event Action<GameObject> Triggered;

    private Collider2D _collider;
    [SerializeField]
    private string[] _tagTriggers;
    [SerializeField]
    private UnityEvent _onTriggered;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        foreach (var tag in _tagTriggers) {
            if (other.CompareTag(tag)) {
                Triggered?.Invoke(other.gameObject);
                _onTriggered?.Invoke();
            }
        }
    }
}