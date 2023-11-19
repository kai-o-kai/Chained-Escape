using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour {
    [SerializeField] private Button _button;

    private void Awake() {
        _button.onClick.AddListener(() => LevelTransitionManager.Instance.TransitionToScene(0, false));
    }
}