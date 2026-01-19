using UnityEngine;

public abstract class PuzzleBehaviour : MonoBehaviour {
  public Puzzle scriptableObject;
  [SerializeField] protected ParticleSystem confetti;
  [SerializeField] protected AudioSource confettiAudio;

  public virtual void Complete() {
    confetti.Play();
    confettiAudio.Play();
    
    var pair = PuzzleManager.Instance.puzzles.entries.Find(x => x.key == scriptableObject);
    if (pair != null) {
      pair.value = true;
    }
  }
}
