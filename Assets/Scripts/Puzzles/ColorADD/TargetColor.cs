using UnityEngine;

public class TargetColor : MonoBehaviour {
  public Renderer crystalRenderer;
  public Color targetColor;

  private void Start() {
    targetColor = GenerateRandomTarget();
    crystalRenderer.material.color = targetColor;
  }

  private Color GenerateRandomTarget() {
    int mask = Random.Range(1, 8);
    targetColor = ColorUtility.ToRGB(
      (mask & 1) != 0,
      (mask & 2) != 0,
      (mask & 4) != 0
    );

    crystalRenderer.material.color = targetColor;
    return targetColor;
  }
}