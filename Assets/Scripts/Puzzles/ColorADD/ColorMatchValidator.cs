using UnityEngine;
using UnityEngine.Serialization;


public class ColorMatchValidator : MonoBehaviour {
  public ColorCore resultCore;
  public TargetColor target;
  public float tolerance = 0.05f;
  public ParticleSystem confetti;
  public AudioSource confettiAudio;
  public Door door;

  private bool _hasSetColorblindness;

  public bool CheckColors() {
    if (ColorsMatch(resultCore.GetCurrentColor(), target.targetColor)) {
      CompletePuzzle();
    }

    return true;
  }

  private bool ColorsMatch(Color coreColor, Color targetColor) {
    return Vector3.Distance(new Vector3(coreColor.r, coreColor.g, coreColor.b), new Vector3(targetColor.r, targetColor.g, targetColor.b)) < tolerance;
  }
  
  [ContextMenu("Complete")]
  private void CompletePuzzle() {
    door.Open();
    if (!_hasSetColorblindness) {
      var colorblind = ColorblindManager.Instance.colorblindQueue.Dequeue();
      ColorblindManager.Instance.SetColorblindness(colorblind);
      _hasSetColorblindness = true;
    }
    confettiAudio.Play();
    confetti.Play();
    target.StartNewTarget();
  }
}