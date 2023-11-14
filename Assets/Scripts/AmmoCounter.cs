using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour {
    public static AmmoCounter Instance => s_instance ?? Instantiate(ReferenceManager.Instance.AmmoCounterPrefab);
    private static AmmoCounter s_instance;

    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private GameObject _container;

    private void Awake() {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Show() {
        _container.SetActive(true);
    }
    public void Hide() {
        _container.SetActive(false);
    }
    public void UpdateData(int current, int max) {
        string text = $"{current} / {max}";
        _ammoText.SetText(text);
    }
}