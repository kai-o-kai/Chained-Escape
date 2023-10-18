using System.Collections;
using UnityEngine;
using static UnityEngine.KeyCode;

public abstract class Gun : IWeapon {
    protected PlayerItemUser _player;

    public Gun(PlayerItemUser itemWielder) {
        _player = itemWielder;
    }

    public abstract void OnFireKeyEnd();
    public abstract void OnFireKeyStart();
    public abstract void OnReloadKeyPress();
}
public class SemiAutomaticGun : Gun {
    private const float FIREINTERVALSECONDS = 0.3f;

    private bool _readyToFire = true;

    public SemiAutomaticGun(PlayerItemUser itemWielder) : base(itemWielder) {

    }

    public override void OnFireKeyEnd() {
      throw new System.NotImplementedException("Fire Key Event Handlers are not implemented yet");
    }

    public override void OnFireKeyStart() {
        if (!_readyToFire) { return; }
        _readyToFire = false;
        _player.StartCoroutine(ReReadyFire(new WaitForSeconds(FIREINTERVALSECONDS)));
    }
    private IEnumerator ReReadyFire(WaitForSeconds wait) {
        yield return wait;
        _readyToFire = true;
    }

    public override void OnReloadKeyPress() {
        KeyCode[] keys = { I, J, K, L };
        ReloadManager.ReloadData data = new ReloadManager.ReloadData(keys, () => Debug.Log("Reload Complete"));
        ReloadManager.Instance.Reload(data);
    }
}