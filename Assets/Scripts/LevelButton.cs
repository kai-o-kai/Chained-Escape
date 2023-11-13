using UnityEngine;

public class LevelButton : MonoBehaviour, IDamagable {
    private const string PRESSANIMATIONNAME = "press";

    [SerializeField]
    private Animator _anim;
    private GameObject _hitByBulletFx;

    public void OnHitByBaton(Baton baton) {
        return;
    }

    public void OnHitByBullet(Bullet bullet) {
        _anim.Play(PRESSANIMATIONNAME);
        _hitByBulletFx = _hitByBulletFx ?? ReferenceManager.Instance.BulletHitEntityParticle;
        Destroy(Instantiate(_hitByBulletFx, bullet.transform.position, bullet.transform.rotation), 2f);
        Destroy(bullet.gameObject);
    }
}