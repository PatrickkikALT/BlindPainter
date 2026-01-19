using UnityEngine;

public class PuzzleManager : MonoBehaviour {
  public SerializableDictionary<Puzzle, bool> puzzles = new();
  public ParticleSystem ringConfetti;
  public AudioSource confettiAudio; 
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
      print("won");
      ringConfetti.Play();
      confettiAudio.Play();
    }

    return _ringsCompleted;
  }

  public void RemoveFinishedRing() => _ringsCompleted--;



}