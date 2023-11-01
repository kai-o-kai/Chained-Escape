using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovementCheckShadow : MonoBehaviour {
    private LayerMask WALLLAYER;
    public bool CurrentPositionValid => CurrentPositionValidFunc();

    [Header("Step Pivots")]
    [SerializeField] private Transform _leftStep;
    [SerializeField] private Transform _rightStep;
    [SerializeField] private Transform _stepPivotContainer;

    private Collider2D _collider;
    private ContactFilter2D _filter = new ContactFilter2D();

    private void Awake() {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        WALLLAYER = LayerMask.GetMask("Walls");
        _filter.SetLayerMask(WALLLAYER);
    }

    private bool CurrentPositionValidFunc() {
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new();
        filter.SetLayerMask(WALLLAYER);
        filter.useLayerMask = true;
        _collider.OverlapCollider(filter, results);

        return results[0] == null;
    }
    public void LeftStep(float rotDegrees) {
        transform.RotateAround(_leftStep.position, Vector3.forward, rotDegrees);
    }
    public void RightStep(float rotDegrees) {
        transform.RotateAround(_rightStep.position, Vector3.forward, -rotDegrees);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == WALLLAYER) {
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == WALLLAYER) {
        }
    }
    public void ResetPosAndRot(Transform player, Transform left, Transform right) {
        _leftStep.position = left.position;
        _leftStep.rotation = left.rotation;
        _rightStep.position = right.position;
        _rightStep.rotation = right.rotation;
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}