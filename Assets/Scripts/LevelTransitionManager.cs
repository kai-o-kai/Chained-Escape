using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelTransitionManager : MonoBehaviour {
    private const string ENTERSCENEANIM = "circleTransitionEnterScene";
    private const string EXITSCENEANIM = "circleTransitionExitScene";

    public static LevelTransitionManager Instance { get {
        if (s_instance == null) {
            s_instance = Instantiate(ReferenceManager.Instance.LevelTransitionManagerPrefab);
        }
        return s_instance;
    } }
    private static LevelTransitionManager s_instance;

    public bool NextSceneExists => SceneManager.GetSceneByBuildIndex(NextSceneIndex) != null;
    public int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;
    public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
    
    [SerializeField]
    private float _fadeTimeSeconds;

    private Animator _animator;

    private void Awake() {
        s_instance = this;
        _animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }
    public void TransitionToScene(int sceneIndex, bool bypassFade) {
        ValidateParameters();
        if (bypassFade) {
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
        _animator.Play(EXITSCENEANIM);
        yield return new WaitForSeconds(_fadeTimeSeconds);
        SceneManager.LoadScene(index);
        yield return new WaitForSeconds(0.5f);
        _animator.Play(ENTERSCENEANIM);
    }
    #if UNITY_EDITOR
    [MenuItem("Debug/Transition To Next Scene Fade")]
    public static void DebugTransitionNextSceneFade() {
        Instance.TransitionToScene(Instance.NextSceneIndex, false);
    }
    [MenuItem("Debug/Transition To Next Scene No Fade")]
    public static void DebugTransitionNextSceneNoFade() {
        Instance.TransitionToScene(Instance.NextSceneIndex, true);
    }
    #endif
}