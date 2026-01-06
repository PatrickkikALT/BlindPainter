using UnityEngine;

public class TargetColor : MonoBehaviour {
  public Renderer crystalRenderer;
  public Color targetColor;

  private void Start() {
    StartNewTarget();
  }

  private Color GenerateRandomTarget() {
    int[] twoColorMasks = { 3, 5, 6 }; // 3=011 (R+Y), 5=101 (R+B), 6=110 (Y+B)

    int mask = twoColorMasks[Random.Range(0, twoColorMasks.Length)];

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