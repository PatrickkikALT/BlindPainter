using UnityEngine;

public class PuzzleManager : MonoBehaviour {
  public SerializableDictionary<Puzzle, bool> puzzles = new();
  public ParticleSystem ringConfetti;
  public AudioSource confettiAudio;
  public Door ringDoor;
  private bool _ringSetColorblindness;
  private int _ringsCompleted;

  public static PuzzleManager Instance;
  public CablePuzzle cablePuzzle;


  public void Awake() {
    Instance = this;
  }
  
  public int FinishRing(RingStick stick) {
    _ringsCompleted++;
    stick.completed = true;
    foreach (var ring in stick.rings) {
      ring.rb.isKinematic = true;
    }

    if (_ringsCompleted >= 3) {
      Complete();
    }

    return _ringsCompleted;
  }

  public void RemoveFinishedRing() => _ringsCompleted--;

  [ContextMenu("Complete")]
  public void Complete() {
    ringConfetti.Play();
    confettiAudio.Play();
    ringDoor.Open();
    if (!_ringSetColorblindness) {
      var colorblind = ColorblindManager.Instance.colorblindQueue.Dequeue();
      ColorblindManager.Instance.SetColorblindness(colorblind);
      _ringSetColorblindness = true;
    }
    
  }


}