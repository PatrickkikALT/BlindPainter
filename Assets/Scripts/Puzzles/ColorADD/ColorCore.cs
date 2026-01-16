using System.Collections.Generic;
using UnityEngine;

public class ColorCore : MonoBehaviour {
  public Renderer coreRenderer;

  private HashSet<ColorAdd> _activeColors = new();

  public void AddColor(ColorAdd color) {
    _activeColors.Add(color);
    UpdateColor();
  }

  public void RemoveColor(ColorAdd color) {
    _activeColors.Remove(color);
    UpdateColor();
  }

  private void UpdateColor() {
    if (_activeColors.Count == 0) {
      coreRenderer.material.color = Color.white;
      return;
    }
    bool r = _activeColors.Contains(ColorAdd.Red);
    bool y = _activeColors.Contains(ColorAdd.Yellow);
    bool b = _activeColors.Contains(ColorAdd.Blue);

    Color result = ColorUtility.ToRGB(r, y, b);

    coreRenderer.material.color = result;
  }

  public Color GetCurrentColor() => coreRenderer.material.color;
}