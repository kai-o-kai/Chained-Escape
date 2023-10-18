using UnityEngine;

[CreateAssetMenu(fileName = "New Reference Manager", menuName = "Scriptable Objects/Reference Manager", order = 5)]
public class ReferenceManager : ScriptableObject {
    public static ReferenceManager Instance => s_instance ?? Resources.Load("Reference Manager") as ReferenceManager;
    private static ReferenceManager s_instance;

    [field: SerializeField] public ReloadManager ReloadManagerPrefab { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
}
