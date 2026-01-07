using UnityEngine;

public class TargetColor : MonoBehaviour {
  public Renderer crystalRenderer;
  public Color targetColor;

  private void Start() {
    StartNewTarget();
  }

  private Color GenerateRandomTarget() {
    int mask = Random.Range(1, 7);

    targetColor = ColorUtility.ToRGB(
      (mask & (int)ColorAdd.Red) != 0,
      (mask & (int)ColorAdd.Yellow) != 0,
      (mask & (int)ColorAdd.Blue) != 0
    );

    crystalRenderer.material.color = targetColor;
    return targetColor;
  }


  public void StartNewTarget() {
    targetColor = GenerateRandomTarget();
    crystalRenderer.material.color = targetColor;
  }
}