using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.KeyCode;

public class ReloadManager : MonoBehaviour {
    public static ReloadManager Instance => s_instance ?? Instantiate(ReferenceManager.Instance.ReloadManagerPrefab);
    private static ReloadManager s_instance;

    [SerializeField]
    private Image _keyImage;
    [SerializeField]
    private KeyImages _sprites;

    private ReloadData _currentReload;
    private Inputs _inputs;

    private void Awake() {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
        _inputs = new Inputs();
        _keyImage.enabled = false;
    }
    private void Start() {
        _inputs.Player.ReloadUp.performed += (ctx) => _currentReload?.ProcessKeyPress(I, _keyImage);
        _inputs.Player.ReloadLeft.performed += (ctx) => _currentReload?.ProcessKeyPress(J, _keyImage);
        _inputs.Player.ReloadDown.performed += (ctx) => _currentReload?.ProcessKeyPress(K, _keyImage);
        _inputs.Player.ReloadRight.performed += (ctx) => _currentReload?.ProcessKeyPress(L, _keyImage);
    }
    private void OnEnable() {
        _inputs.Enable();
    }
    private void OnDisable() {
        _inputs.Disable();
    }

    public void Reload(ReloadData data) {
        if (_currentReload != null) {
            throw new InvalidOperationException("Reload Manager already has a reload in progress! Ignoring new call. Tools should not be capable of sending a reload while a reload is already in progress.");
            // The C# compiler registers this return statement as unreachable, even though catch statements exist... So pragma goes here.
#pragma warning disable CS0162
            return;
#pragma warning restore CS0162
        }
        _currentReload = data;
        _currentReload.Complete += () => {
            _currentReload = null;
            _keyImage.enabled = false;
        };
        _currentReload.StartReload(_keyImage, _sprites);
    }
    public class ReloadData {
        public event Action Complete;

        private KeyCode[] _sequence;
        private int _index = 0;

        private KeyImages _images;

        public ReloadData(KeyCode[] sequence, params Action[] onComplete) {
            _sequence = sequence;
            foreach (var toAdd in onComplete) {
                Complete += toAdd;
            }
        }
        public void StartReload(Image toChange, KeyImages images) {
            _index = 0;
            _images = images;
            toChange.sprite = _sequence[_index] switch {
                I => _images.Up,
                J => _images.Left,
                K => _images.Down,
                L => _images.Right,
                _ => null
            };
            toChange.enabled = true;
        }
        public void ProcessKeyPress(KeyCode pressed, Image toChange) {
            KeyCode wantedKey = _sequence[_index];
            if (wantedKey == pressed) {
                _index++;
                bool reloadIsComplete = (_index == _sequence.Length);
                if (reloadIsComplete) {
                    Complete?.Invoke();
                    return;
                }
                toChange.sprite = _sequence[_index] switch {
                    I => _images.Up,
                    J => _images.Left,
                    K => _images.Down,
                    L => _images.Right,
                    _ => null
                };
            }
        }
    }
    [System.Serializable]
    public struct KeyImages {
        [field: SerializeField] public Sprite Up { get; private set; }
        [field: SerializeField] public Sprite Left { get; private set; }
        [field: SerializeField] public Sprite Down { get; private set; }
        [field: SerializeField] public Sprite Right { get; private set; }
    }
}