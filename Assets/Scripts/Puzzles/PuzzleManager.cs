using UnityEngine;

public class PuzzleManager : MonoBehaviour {
  public SerializableDictionary<Puzzle, bool> puzzles = new();
  [SerializeField] private ParticleSystem confetti;
  private int _ringsCompleted;

  public static PuzzleManager Instance;

  [Header("Cable Puzzle")] public int totalConnectionsRequired;
  private int _currentConnections;

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
      confetti.Play();
    }

    return _ringsCompleted;
  }

  public void RemoveFinishedRing() => _ringsCompleted--;


  public void RegisterConnection() {
    _currentConnections++;

    if (_currentConnections >= totalConnectionsRequired) {
      CompletePuzzle();
    }
  }

  private void CompletePuzzle() {
    confetti.Play();
  }
}