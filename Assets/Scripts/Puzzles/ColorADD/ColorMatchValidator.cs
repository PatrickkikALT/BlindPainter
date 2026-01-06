using UnityEngine;


public class ColorMatchValidator : MonoBehaviour {
  public ColorCore core;
  public TargetColor target;
  public float tolerance = 0.05f;
  public ParticleSystem confetti;

  private void FixedUpdate() {
    if (ColorsMatch(core.GetCurrentColor(), target.targetColor)) {
      CompletePuzzle();
    }
  }

  private bool ColorsMatch(Color coreColor, Color targetColor) {
    return Vector3.Distance(new Vector3(coreColor.r, coreColor.g, coreColor.b), new Vector3(targetColor.r, targetColor.g, targetColor.b)) < tolerance;
  }

  private void CompletePuzzle() {
    confetti.Play();
  }
}