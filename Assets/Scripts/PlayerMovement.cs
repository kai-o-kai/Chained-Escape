using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool Enabled { get; set; } = true;


    [Header("Step Pivots")]
    [SerializeField] private Transform _leftStep;
    [SerializeField] private Transform _rightStep;
    [Header("Values")]
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnStep;
    [SerializeField][Range(-180f, 180f)] private float _rotateAmountOnBackStep;
    [Header("Other")]
    [SerializeField] private PlayerMovementCheckShadow _checkShadow;

    private Inputs _inputs;
    private bool _backStepPressed => _inputs.Player.StepBack.ReadValue<float>() == 1f;

    private void Awake() {
        _inputs = new Inputs();
    }
    private void Start() {
        _inputs.Player.StepLeft.performed += (_) => OnLeftStepPress();
        _inputs.Player.StepRight.performed += (_) => OnRightStepPress();

        EventSystem.Instance.PlayerEnteredLevelEndElevator += () => Enabled = false;
    }
    private void OnEnable() {
        _inputs.Enable();
    }
    private void OnDisable() {
        _inputs.Disable();
    }
    private void LeftStep(float rotDegrees) {
        _checkShadow.LeftStep(rotDegrees);
        Physics2D.SyncTransforms();
        if (_checkShadow.CurrentPositionValid) {
            transform.RotateAround(_leftStep.position, Vector3.forward, rotDegrees);
            Physics2D.SyncTransforms();
        }
        _checkShadow.ResetPosAndRot(transform, _leftStep, _rightStep);
        Physics2D.SyncTransforms();
    }
    private void RightStep(float rotDegrees) {
        _checkShadow.RightStep(rotDegrees);
        Physics2D.SyncTransforms();
        if (_checkShadow.CurrentPositionValid) {
            transform.RotateAround(_rightStep.position, Vector3.forward, -rotDegrees);
            Physics2D.SyncTransforms();
        }
        _checkShadow.ResetPosAndRot(transform, _leftStep, _rightStep);
        Physics2D.SyncTransforms();
    }
    private void OnLeftStepPress() {
        if (!Enabled) { return; }

        if (!_backStepPressed) {
            LeftStep(_rotateAmountOnStep);
        } else {
            LeftStep(-_rotateAmountOnBackStep);
        }
    }
    private void OnRightStepPress() {
        if (!Enabled) { return; }

        if (!_backStepPressed) {
            RightStep(_rotateAmountOnStep);
        } else {
            RightStep(-_rotateAmountOnBackStep);
        }
    }
}