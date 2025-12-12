using UnityEngine;
[RequireComponent(typeof(Collider))]
public class PaintBucket : MonoBehaviour {
  private Color _color;
  [SerializeField] private Renderer paint;
  public Color Color { 
    get => _color;
    set {
      _color = value;
      OnSet();
    }
  }

  public void OnSet() {
    paint.material.color = _color;
  }
}
