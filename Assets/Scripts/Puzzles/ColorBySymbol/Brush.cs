using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Brush : XRGrabInteractable {
  public Color currentBrushColor;

  private void OnCollisionEnter(Collision other) {
    if (other.gameObject.TryGetComponent(out PaintBucket bucket)) {
      currentBrushColor = bucket.GetColor();
    }
  }
}
