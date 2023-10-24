using UnityEngine;

public class LevelEndElevator : MonoBehaviour {
    private const string DOORCLOSEANIMNAME = "close";

    [SerializeField]
    private EntityPassTrigger _trigger;
    [SerializeField]
    private Animator _doorAnimator;

    private void Start() {
        _trigger.Triggered += OnElevatorThresholdPassed;
    }
    private void OnElevatorThresholdPassed(GameObject objectCrossing) {
        if (!objectCrossing.CompareTag("Player")) { return; }

        _doorAnimator.Play(DOORCLOSEANIMNAME);
    }
}