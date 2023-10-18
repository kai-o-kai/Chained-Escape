using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Step Pivots")]
    [SerializeField] private Transform _leftStep;
    [SerializeField] private Transform _rightStep;
    [SerializeField] private Transform _stepPivotContainer;
    [Header("Values")]
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnStep;

    private Inputs _inputs;
    private Transform _currentPivot => _isPivotedToFoot ? transform.parent : null;
    private bool _isPivotedToFoot => (transform.parent == _leftStep) || (transform.parent == _rightStep);

    private void Awake() {
        _inputs = new Inputs();
    }
    private void Start() {
        _inputs.Player.StepLeft.performed += (_) => LeftStep(_rotateAmountOnStep);
        _inputs.Player.StepRight.performed += (_) => RightStep(_rotateAmountOnStep);
        _inputs.Player.StepBack.performed += (_) => OnBackStep(-_rotateAmountOnStep);
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
    private void OnBackStep(float rotDegrees) {
        if (!_isPivotedToFoot) {
            bool doLeftStep = Random.value > 0.5f;
            if (doLeftStep) {
                LeftStep(rotDegrees);
            } else {
                RightStep(rotDegrees);
            }
            return;
        }
        if (_currentPivot == _leftStep) {
            RightStep(rotDegrees);
        } else {
            LeftStep(rotDegrees);
        }
    }
}