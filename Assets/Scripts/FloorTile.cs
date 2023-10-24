using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FloorTile : MonoBehaviour {
    [SerializeField]
    private Color _minColor;
    [SerializeField]
    private Color _maxColor;

    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        Color color = Color.Lerp(_minColor, _maxColor, Random.value);
        _renderer.color = color;
    }
}