using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelEndElevator : MonoBehaviour {
    private const string CLOSEANIMATIONNAME = "close";
    
    private Animator _doorAnimator;

    [SerializeField]
    private EntityPassTrigger _trigger;
    
    private void Awake() {
        _doorAnimator = GetComponent<Animator>();
    }

    private void Start() {
        _trigger.Triggered += OnElevatorThresholdPassed;
    }
    private void OnElevatorThresholdPassed(GameObject objectCrossing) {
        if (!objectCrossing.CompareTag("Player")) { return; }

        _doorAnimator.Play(CLOSEANIMATIONNAME);
        EventSystem.Instance.OnPlayerEnterLevelEndElevator();
    }
}