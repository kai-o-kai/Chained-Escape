using UnityEngine;

public class OpeningCutscene : MonoBehaviour {
    public void OnCutsceneEnd() {
        LevelTransitionManager.Instance.TransitionToScene(LevelTransitionManager.Instance.NextSceneIndex, false);
    }
}