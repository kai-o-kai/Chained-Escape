using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WeaponLaser : MonoBehaviour {
    
    private LineRenderer _line;

    private void Awake() {
        _line = GetComponent<LineRenderer>();
    }

    private void Update() {
        _line.SetPosition(0, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit.collider is null) {
            _line.SetPosition(1, transform.up * 1000000f);
            return;
        }
        _line.SetPosition(1, hit.point);
    }
}