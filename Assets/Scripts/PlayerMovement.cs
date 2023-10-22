using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Step Pivots")]
    [SerializeField] private Transform _leftStep;
    [SerializeField] private Transform _rightStep;
    [SerializeField] private Transform _stepPivotContainer;
    [Header("Values")]
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnStep;
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnBackStep;

    private Inputs _inputs;
    private bool _backStepPressed => _inputs.Player.StepBack.ReadValue<float>() == 1f;

    private void Awake() {
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
        transform.parent = _leftStep;
        _rightStep.parent = transform;
        _leftStep.eulerAngles = new Vector3(0f, 0f, _leftStep.eulerAngles.z + rotDegrees);
        _rightStep.parent = _stepPivotContainer;
    }
    private void RightStep(float rotDegrees) {
        transform.parent = _rightStep;
        _leftStep.parent = transform;
        _rightStep.eulerAngles = new Vector3(0f, 0f, _rightStep.eulerAngles.z - rotDegrees);
        _leftStep.parent = _stepPivotContainer;
    }
    private void OnLeftStepPress() {
        if (!_backStepPressed) {
            LeftStep(_rotateAmountOnStep);
        } else {
            LeftStep(-_rotateAmountOnBackStep);
        }
    }
    private void OnRightStepPress() {
        if (!_backStepPressed) {
            RightStep(_rotateAmountOnStep);
        } else {
            RightStep(-_rotateAmountOnBackStep);
        }
    }
}