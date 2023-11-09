using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animator))]
public class CameraManager : MonoBehaviour {
    public static CameraManager Instance { get; private set; }

    private const string SHAKEANIMATIONNAME = "shake";
    private const string EXITSCENEANIMATIONNAME = "circleTransitionExitScene";
    private const string ENTERSCENEANIMATIONNAME = "circleTransitionEnterScene";

    private const float WAYPOINTREACHEDDISTANCE = 0.05f;

    [SerializeField]
    private CameraWaypoint[] _waypoints;
    [SerializeField]
    [Range(0f, 100f)]
    private float _speed;
    [SerializeField]
    private bool _cameraMovementEnabled;

    private Animator _animator;
    private int _waypointIndex;
    private CameraWaypoint _currentWaypoint => _waypoints[_waypointIndex];

    private void Awake() {
        _animator = GetComponent<Animator>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update() {
        MoveCameraAlongWaypoints();

        void MoveCameraAlongWaypoints() {
            if (!_cameraMovementEnabled) { return; }

            transform.position = Vector2.MoveTowards(transform.position, _currentWaypoint.transform.position, Time.deltaTime * _speed);
            if (Vector2.Distance(transform.position, _currentWaypoint.transform.position) < WAYPOINTREACHEDDISTANCE) {
                _currentWaypoint.ReachCameraWaypoint(this);
                bool reachedEndOfWaypoints = (_waypointIndex + 1) == _waypoints.Length;
                if (reachedEndOfWaypoints)
                {
                    _cameraMovementEnabled = false;
                    return;
                }
                _waypointIndex++;
            }
        }
    }
    public void PlayExitSceneTransition() {
        _animator.Play(EXITSCENEANIMATIONNAME);
    }
    public void PlayEnterSceneTransition() {
        _animator.Play(ENTERSCENEANIMATIONNAME);
    }
    public void ShakeCamera() {
        _animator.Play(SHAKEANIMATIONNAME);
    }

    #if UNITY_EDITOR
    [MenuItem("Debug/Play Exit Scene Transition")]
    public static void DebugPlayExitSceneTransition() {
        Instance.PlayExitSceneTransition();
    }
    [MenuItem("Debug/Play Enter Scene Transition")]
    public static void DebugPlayEnterSceneTransition() {
        Instance.PlayEnterSceneTransition();
    }
    [MenuItem("Debug/Shake Camera")]
    public static void DebugShakeCamera() {
        Instance.ShakeCamera();
    }
    #endif
}