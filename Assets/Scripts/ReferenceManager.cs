using UnityEngine;

[CreateAssetMenu(fileName = "New Reference Manager", menuName = "Scriptable Objects/Reference Manager", order = 5)]
public class ReferenceManager : ScriptableObject {
    public static ReferenceManager Instance => s_instance ?? Resources.Load("Reference Manager") as ReferenceManager;
    private static ReferenceManager s_instance;

    [field: SerializeField] public ReloadManager ReloadManagerPrefab { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public GameObject BulletHitEntityParticle { get; private set; }
    [field: SerializeField] public AmmoCounter AmmoCounterPrefab { get; private set; }
    [field: SerializeField] public LevelTransitionManager LevelTransitionManagerPrefab { get; private set; }
    [field: SerializeField] public AudioClip PistolShot { get; private set; }
    [field: SerializeField] public AudioClip PistolReload { get; private set; }
    [field: SerializeField] public AudioClip PistolDryfire { get; private set; }
    [field: SerializeField] public AudioClip ChainRattle { get; private set; }
    [field: SerializeField] public AudioClip BatonHit { get; private set; }
    [field: SerializeField] public AudioClip MedkitPickup { get; private set; }
}
