using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animator))]
public class CameraManager : MonoBehaviour {
    public static CameraManager Instance { get; private set; }

    private const string SHAKEANIMATIONNAME = "shake";

    
    [SerializeField]
    private bool _cameraMovementEnabled;
    [SerializeField]
    private Transform _target;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        Instance = this;
    }
    private void Update() {
        if (_cameraMovementEnabled) {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }
    }
    private void OnDestroy() {
        Instance = null;
    }
    public void ShakeCamera() {
        _animator.Play(SHAKEANIMATIONNAME);
    }
    #if UNITY_EDITOR
    [MenuItem("Debug/Shake Camera")]
    public static void DebugShakeCamera() {
        Instance.ShakeCamera();
    }
    #endif
}