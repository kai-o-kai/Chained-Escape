using System.Collections;
using UnityEngine;
using static UnityEngine.KeyCode;

public abstract class Gun : IPlayerWeapon {
    protected PlayerItemUser _player;

    public Gun(PlayerItemUser itemWielder) {
        _player = itemWielder;
    }

    public abstract void OnFireKeyEnd();
    public abstract void OnFireKeyStart();
    public abstract void OnReloadKeyPress();
}
public class SemiAutomaticGun : Gun
{
    private const float FIREINTERVALSECONDS = 0.3f;
    private const float BULLETSPEED = 50f;
    private const float BULLETDAMAGE = 20f;
    private static int BULLETLAYER;
    private const int AMMOPERMAG = 15;
    private const float RECOILDEGREES = 10f;

    private bool _readyToFire;
    private Bullet _bulletPrefab;
    private int _currentAmmo;
    private AudioClip _shootSound;
    private AudioClip _reloadSound;
    private AudioClip _dryFireSound;

    public SemiAutomaticGun(PlayerItemUser itemWielder) : base(itemWielder) {
        _bulletPrefab = ReferenceManager.Instance.BulletPrefab;
        _readyToFire = true;
        _currentAmmo = AMMOPERMAG;
        BULLETLAYER = LayerMask.NameToLayer("PlayerBullet");
        _shootSound = ReferenceManager.Instance.PistolShot;
        _reloadSound = ReferenceManager.Instance.PistolReload;
        _dryFireSound = ReferenceManager.Instance.PistolDryfire;

        AmmoCounter.Instance.UpdateData(_currentAmmo, AMMOPERMAG);
        AmmoCounter.Instance.Show();
    }

    public override void OnFireKeyEnd() {
        return;
    }

    public override void OnFireKeyStart() {
        if (!_readyToFire) { return; }
        if (_currentAmmo <= 0) {
            _player.AudioSource.PlayOneShot(_dryFireSound);
            return; 
        }

        _readyToFire = false;
        _player.StartCoroutine(ReReadyFire(new WaitForSeconds(FIREINTERVALSECONDS)));
        _currentAmmo--;
        AmmoCounter.Instance.UpdateData(_currentAmmo, AMMOPERMAG);
        _player.AudioSource.PlayOneShot(_shootSound);
        Object.Instantiate(_bulletPrefab, _player.FirePoint.position, _player.FirePoint.rotation).Shoot(BULLETSPEED, BULLETLAYER, BULLETDAMAGE, 0f);
        if (_player.TryGetComponent<PlayerMovement>(out var movement)) {
            movement.LeftStep(-RECOILDEGREES);
        }
    }
    private IEnumerator ReReadyFire(WaitForSeconds wait) {
        yield return wait;
        _readyToFire = true;
    }

    public override void OnReloadKeyPress() {
        if (_currentAmmo == AMMOPERMAG) { return; }

        KeyCode[] keys = { I, J, K, L };
        _player.AudioSource.PlayOneShot(_reloadSound);
        ReloadManager.ReloadData data = new ReloadManager.ReloadData(keys, () => {
            _currentAmmo = AMMOPERMAG;
            AmmoCounter.Instance.UpdateData(_currentAmmo, AMMOPERMAG);
        });
        ReloadManager.Instance.Reload(data);
    }
}