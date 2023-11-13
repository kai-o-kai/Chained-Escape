using UnityEngine;

public class DropShadow : MonoBehaviour {
    [SerializeField] private Vector2 _offset;

    private Transform _shadowedObject;

    private void Awake() {
        _shadowedObject = transform.parent;
    }
    private void Update() {
        transform.position = _shadowedObject.position + (Vector3)_offset;
        transform.rotation = _shadowedObject.rotation;
    }
}