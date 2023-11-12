using UnityEngine;
using System.Collections;

public class GunnerEnemy : Enemy {

    private enum GUNNERENEMYWEAPON {
        AK47 = 0
    }

    private const float TURNSPEED = 5f;

    private static readonly GunImplementations.IWeapon[] WEAPONS = new GunImplementations.IWeapon[] {
        new GunImplementations.AK47()
    };
    private bool _firing;
    [SerializeField] private GUNNERENEMYWEAPON _weaponIndex;
    private GunImplementations.IWeapon _weapon;


    protected override void Start() {
        base.Start();
        _weapon = WEAPONS[(int)_weaponIndex];
        _weapon.Initialize(this);
    }
    protected override void Update() {
        base.Update();
        if (CanSeePlayer()) {
            if (!_firing && TurnedToPlayer()) {
                _weapon.OnStartFiring();
                _firing = true;
            }
            TurnToPlayer(TURNSPEED);
        } else {
            if (_firing) {
                _weapon.OnStopFiring();
                _firing = false;
            }
            Destination = RememberedPlayerPosition;
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
            private const float FIREINTERVAL = 0.75f;
            private const float RELOADTIME = 3f;
            private const float BULLETSPEED = 50f;
            private const float BULLETDAMAGE = 20f;
            private static int BULLETLAYER;

            private int _currentAmmo;
            private GunnerEnemy _data;
            private Coroutine _currentTask;
            private WaitForSeconds _fireIntervalWait;
            private Bullet _bulletPrefab;

            public void Initialize(GunnerEnemy data) {
                _currentAmmo = AMMOPERMAG;
                _data = data;
                _fireIntervalWait = new WaitForSeconds(FIREINTERVAL);
                _bulletPrefab = ReferenceManager.Instance.BulletPrefab;
                BULLETLAYER = LayerMask.NameToLayer("EnemyBullet");
                
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
                Instantiate(_bulletPrefab, _data.transform.position, _data.transform.rotation).Shoot(BULLETSPEED, BULLETLAYER, BULLETDAMAGE);
            }
        }
    }
}
