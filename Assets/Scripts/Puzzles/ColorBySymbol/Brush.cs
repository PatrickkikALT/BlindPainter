using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
[RequireComponent(typeof(Collider))]
public class Brush : XRGrabInteractable {
  [SerializeField] private Renderer renderer;
  private Color _color;
  public Color currentBrushColor {
    get => _color;
    set {
      _color = value;
      renderer.material.color = value;
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.TryGetComponent(out PaintBucket bucket)) {
      currentBrushColor = bucket.Color;
    }
  }
}
