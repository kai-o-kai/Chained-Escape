using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour {
    public static LevelTransitionManager Instance { get {
        if (s_instance == null) {
            GameObject o = new("Level Transition Manager");
            s_instance = o.AddComponent<LevelTransitionManager>();
        }
        return s_instance;
    } }
    private static LevelTransitionManager s_instance;

    public bool NextSceneExists => SceneManager.GetSceneByBuildIndex(NextSceneIndex) != null;
    public int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;
    
    private float _fadeSeconds;

    private void Awake() {
        s_instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start() {
        _fadeSeconds = CameraManager.Instance.GetFadeAnimationSeconds();
    }
    public void TransitionToScene(int sceneIndex, bool bypassFade) {
        ValidateParameters();
        if (!bypassFade) {
            SceneManager.LoadScene(sceneIndex);
            return;
        }
        
        StartCoroutine(C_SceneTransitionWithFade(sceneIndex));
        
        void ValidateParameters() {
            bool sceneExists = SceneManager.GetSceneByBuildIndex(sceneIndex) != null;
            if (!sceneExists) {
                throw new System.ArgumentOutOfRangeException(nameof(sceneIndex), $"No scene found at {sceneIndex} but requested scene to load!");
            }
        }
    }
    private IEnumerator C_SceneTransitionWithFade(int index) {
        CameraManager.Instance.PlayExitSceneTransition();
        yield return new WaitForSeconds(_fadeSeconds);
        SceneManager.LoadScene(index);
        CameraManager.Instance.PlayEnterSceneTransition();
    }
}