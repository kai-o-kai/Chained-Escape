using UnityEngine;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Awake() {
        _playButton.onClick.AddListener(OnPlayButtonPressed);   
        _quitButton.onClick.AddListener(OnQuitButtonPressed);
    }
    private void OnDestroy() {
        _playButton.onClick.RemoveListener(OnPlayButtonPressed);
        _quitButton.onClick.RemoveListener(OnQuitButtonPressed);
    }
    private void OnQuitButtonPressed() => Application.Quit(0);
    private void OnPlayButtonPressed() => LevelTransitionManager.Instance.TransitionToScene(LevelTransitionManager.Instance.NextSceneIndex, false);
}