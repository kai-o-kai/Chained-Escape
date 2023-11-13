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

    private bool _readyToFire;
    private Bullet _bulletPrefab;
    private int _currentAmmo;

    public SemiAutomaticGun(PlayerItemUser itemWielder) : base(itemWielder) {
        _bulletPrefab = ReferenceManager.Instance.BulletPrefab;
        _readyToFire = true;
        _currentAmmo = AMMOPERMAG;
        BULLETLAYER = LayerMask.NameToLayer("PlayerBullet");
    }

    public override void OnFireKeyEnd() {
        return;
    }

    public override void OnFireKeyStart() {
        if (!_readyToFire) { return; }
        if (_currentAmmo <= 0) { return; } // TODO : Play dryfire sound
        _readyToFire = false;
        _player.StartCoroutine(ReReadyFire(new WaitForSeconds(FIREINTERVALSECONDS)));
        _currentAmmo--;
        Object.Instantiate(_bulletPrefab, _player.FirePoint.position, _player.FirePoint.rotation).Shoot(BULLETSPEED, BULLETLAYER, BULLETDAMAGE, 0f);
    }
    private IEnumerator ReReadyFire(WaitForSeconds wait) {
        yield return wait;
        _readyToFire = true;
    }

    public override void OnReloadKeyPress() {
        if (_currentAmmo == AMMOPERMAG) { return; }

        KeyCode[] keys = { I, J, K, L };
        ReloadManager.ReloadData data = new ReloadManager.ReloadData(keys, () => _currentAmmo = AMMOPERMAG);
        ReloadManager.Instance.Reload(data);
    }
}