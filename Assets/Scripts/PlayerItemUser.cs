using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerItemUser : MonoBehaviour {
    public AudioSource AudioSource { get; private set; }

    [field: SerializeField] public Transform FirePoint { get; private set; }

    private Inputs _inputs;
    private IPlayerWeapon _currentWeapon;

    private void Awake() {
        _inputs = new Inputs();
        AudioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        _inputs.Player.Fire.performed += (_) => _currentWeapon.OnFireKeyStart();
        _inputs.Player.Fire.canceled += (_) => _currentWeapon.OnFireKeyEnd();
        _inputs.Player.ReloadStart.performed += (_) => _currentWeapon.OnReloadKeyPress();

        _currentWeapon = new SemiAutomaticGun(this);
    }
    private void OnEnable() {
        _inputs.Enable();
    }
    private void OnDisable() {
        _inputs.Disable();   
    }
}
public interface IPlayerWeapon {
    void OnFireKeyStart();
    void OnFireKeyEnd();
    void OnReloadKeyPress();
}