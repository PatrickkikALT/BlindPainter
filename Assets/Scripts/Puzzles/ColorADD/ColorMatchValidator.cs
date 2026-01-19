using UnityEngine;
using UnityEngine.Serialization;


public class ColorMatchValidator : MonoBehaviour {
  public ColorCore resultCore;
  public TargetColor target;
  public float tolerance = 0.05f;
  public ParticleSystem confetti;
  public AudioSource confettiAudio;

  private void FixedUpdate() {
    if (ColorsMatch(resultCore.GetCurrentColor(), target.targetColor)) {
      CompletePuzzle();
    }
  }

  private bool ColorsMatch(Color coreColor, Color targetColor) {
    return Vector3.Distance(new Vector3(coreColor.r, coreColor.g, coreColor.b), new Vector3(targetColor.r, targetColor.g, targetColor.b)) < tolerance;
  }

  private void CompletePuzzle() {
    confettiAudio.Play();
    confetti.Play();
    target.StartNewTarget();
  }
}