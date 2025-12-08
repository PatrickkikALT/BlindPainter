using UnityEngine;

public class PaintSlot : MonoBehaviour {

  private Color _actualColor;
  [SerializeField] private Renderer _renderer;
  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.TryGetComponent(out Brush brush)) {
      _renderer.material.color = brush.currentBrushColor;
      if (brush.currentBrushColor == _actualColor) {
        //correct
      }
    }
  }
}
