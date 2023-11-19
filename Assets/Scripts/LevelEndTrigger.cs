using UnityEngine;

public class LevelEndTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) { return; }

        EventSystem.Instance.OnPlayerEnterLevelEndElevator();
        Invoke(nameof(TransitionSceneNext), 1f);
    }
    private void TransitionSceneNext() => LevelTransitionManager.Instance.TransitionToScene(LevelTransitionManager.Instance.NextSceneIndex, false);
}