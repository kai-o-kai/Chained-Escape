using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool Enabled = true;

    private Rigidbody2D _rigidbody;

    [Header("Step Pivots")]
    [SerializeField] private Rigidbody2D _leftStep;
    [SerializeField] private Rigidbody2D _rightStep;
    [SerializeField] private Transform _stepPivotContainer;
    [Header("Values")]
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnStep;
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnBackStep;

    private Inputs _inputs;
    private bool _backStepPressed => _inputs.Player.StepBack.ReadValue<float>() == 1f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputs = new Inputs();
    }
    private void Start() {
        _inputs.Player.StepLeft.performed += (_) => OnLeftStepPress();
        _inputs.Player.StepRight.performed += (_) => OnRightStepPress();
    }
    private void OnEnable() {
        _inputs.Enable();
    }
    private void OnDisable() {
        _inputs.Disable();
    }
    private void LeftStep(float rotDegrees) {
        _rigidbody.centerOfMass = _leftStep.transform.position;
        _rigidbody.MoveRotation(_rigidbody.rotation + rotDegrees);
    }
    private void RightStep(float rotDegrees) {
        _rigidbody.centerOfMass = _rightStep.transform.position;
        _rigidbody.MoveRotation(_rigidbody.rotation - rotDegrees);
    }
    private void OnLeftStepPress() {
        if (!Enabled) { return; }

        if (!_backStepPressed) {
            LeftStep(_rotateAmountOnStep);
        } else {
            RightStep(-_rotateAmountOnBackStep);
        }
    }
    private void OnRightStepPress() {
        if (!Enabled) { return; }
        
        if (!_backStepPressed) {
            RightStep(_rotateAmountOnStep);
        } else {
            LeftStep(-_rotateAmountOnBackStep);
        }
    }
}