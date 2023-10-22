using UnityEngine;
using System.Collections;

public class GunnerEnemy : Enemy {
    private static readonly GunImplementations.IWeapon[] WEAPONS = new GunImplementations.IWeapon[] {
        new GunImplementations.AK47()
    };
    private bool _firing;
    private GunImplementations.IWeapon _weapon = WEAPONS[0];

    protected override void Start() {
        base.Start();
        _weapon.Initialize(this);
    }
    private void Update() {
        if (CanSeePlayer()) {
            if (!_firing) {
                _weapon.OnStartFiring();
                _firing = true;
            }
        } else {
            if (_firing) {
                _weapon.OnStopFiring();
                _firing = false;
            }
        }
    }


    private class GunImplementations {
        public interface IWeapon {
            void Initialize(GunnerEnemy data);
            void OnStartFiring();
            void OnStopFiring();
        }
        public class AK47 : IWeapon {
            private const int AMMOPERMAG = 30;
            private const float FIREINTERVAL = 0.5f;
            private const float RELOADTIME = 3f;

            private int _currentAmmo;
            private GunnerEnemy _data;
            private Coroutine _currentTask;
            private WaitForSeconds _fireIntervalWait;

            public void Initialize(GunnerEnemy data) {
                _currentAmmo = AMMOPERMAG;
                _data = data;
                _fireIntervalWait = new WaitForSeconds(FIREINTERVAL);
            }
            public void OnStartFiring() {
                _currentTask = _data.StartCoroutine(FireTask());
            }
            public void OnStopFiring() {
                _data.StopCoroutine(_currentTask);
            }
            private IEnumerator FireTask() {
                while (true) {
                    if (_currentAmmo <= 0) {
                        yield return Reload();        
                    }
                    Fire();
                    yield return _fireIntervalWait;
                }
                
            }
            private IEnumerator Reload() {
                yield return new WaitForSeconds(RELOADTIME);
                _currentAmmo = AMMOPERMAG;
            }
            private void Fire() {
                _currentAmmo--;
                Debug.Log("shooty shoot");
            }
        }
    }
}
