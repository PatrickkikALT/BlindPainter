using UnityEngine;

public abstract class PuzzleBehaviour : MonoBehaviour {
  public Puzzle scriptableObject;
  [SerializeField] protected ParticleSystem confetti;
  [SerializeField] protected AudioSource confettiAudio;
  public Door door;
  private bool _hasSetColorblindness;
  [ContextMenu("Complete")]
  public virtual void Complete() {
    confetti.Play();
    confettiAudio.Play();
    door.Open();
    if (!_hasSetColorblindness) {
      var colorblind = ColorblindManager.Instance.colorblindQueue.Dequeue();
      ColorblindManager.Instance.SetColorblindness(colorblind);
      _hasSetColorblindness = true;
    }
    var pair = PuzzleManager.Instance.puzzles.entries.Find(x => x.key == scriptableObject);
    if (pair != null) {
      pair.value = true;
    }
  }
}
