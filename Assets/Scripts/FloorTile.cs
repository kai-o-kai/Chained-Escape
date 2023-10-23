using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FloorTile : MonoBehaviour {
    [SerializeField]
    private Color _minColor;
    [SerializeField]
    private Color _maxColor;

    private SpriteRenderer _renderer;

    private void Start() {
        float r = Random.Range(_minColor.r, _maxColor.r);
        float g = Random.Range(_minColor.g, _maxColor.g);
        float b = Random.Range(_minColor.b, _maxColor.b);
        Color c = new Color(r, g, b);
        _renderer.color = c;
    }
}