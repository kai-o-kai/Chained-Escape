using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovementCheckShadow : MonoBehaviour {
    private int WALLLAYER;
    
    public bool CurrentPositionValid => _collider.IsTouchingLayers(WALLLAYER);

    [Header("Step Pivots")]
    [SerializeField] private Transform _leftStep;
    [SerializeField] private Transform _rightStep;
    [SerializeField] private Transform _stepPivotContainer;

    private Collider2D _collider;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        WALLLAYER = LayerMask.NameToLayer("Walls");
    }
    public void LeftStep(float rotDegrees) {
        transform.parent = _leftStep;
        _rightStep.parent = transform;
        _leftStep.eulerAngles = new Vector3(0f, 0f, _leftStep.eulerAngles.z + rotDegrees);
        _rightStep.parent = _stepPivotContainer;
    }
    public void RightStep(float rotDegrees) {
        transform.SetParent(_rightStep);
        _leftStep.SetParent(transform);
        _rightStep.eulerAngles = new Vector3(0f, 0f, _rightStep.eulerAngles.z - rotDegrees);
        _leftStep.parent = _stepPivotContainer;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == WALLLAYER) {
            Debug.Log("entered wall");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == WALLLAYER) {
            Debug.Log("exited wall");
        }
    }
}