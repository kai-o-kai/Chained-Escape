using UnityEngine;
using System;

public class GunnerEnemy : Enemy {
    private static readonly GunImplementations.IWeapon[] WEAPONS = new GunImplementations.IWeapon[] {

    };
    private bool _firing;
    private GunImplementations.IWeapon _weapon;

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

    }
}
