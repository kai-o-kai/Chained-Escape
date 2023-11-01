using System;
using UnityEngine;

public class EventSystem : MonoBehaviour {
    public static EventSystem Instance => GetInstance();
    private static EventSystem s_instance;

    private static EventSystem GetInstance() {
        if (s_instance is null) {
            GameObject o = new GameObject("Event System");
            s_instance = o.AddComponent<EventSystem>();
        }
        return s_instance;
    }

    public event Action PlayerEnteredLevelEndElevator;

    public void OnPlayerEnterLevelEndElevator() {
        PlayerEnteredLevelEndElevator?.Invoke();
    }

    private void Awake() {
        s_instance = this;
    }

}