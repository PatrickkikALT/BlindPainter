using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PaintSlot : MonoBehaviour {

  [SerializeField] private Color actualColor;
  [SerializeField] private Renderer renderer;
  private Collider _collider;
  private void Start() {
    renderer = GetComponent<Renderer>();
    _collider = GetComponent<Collider>();
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.TryGetComponent(out Brush brush)) {
      if (brush.currentBrushColor == actualColor) {
        renderer.material.color = brush.currentBrushColor;
        Physics.IgnoreCollision(_collider, collision.collider);
        GameManager.Instance.colorBySymbol.CompleteSlot(this);
      }
      
    }
  }
}
