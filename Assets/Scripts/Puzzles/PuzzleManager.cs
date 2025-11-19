using UnityEngine;

public class PuzzleManager : MonoBehaviour {
  public SerializableDictionary<Puzzle, bool> dictionary = new();

  private int _ringsCompleted;

  public static PuzzleManager Instance;

  public void Awake() {
    Instance = this;
  }
  
  public int FinishRing(RingStick stick) {
    _ringsCompleted++;
    stick.completed = true;
    foreach (var ring in stick.rings) {
      ring.rb.isKinematic = true;
      ring.col.enabled = false;
    }
    if (_ringsCompleted >= 3) {
      print("won");
    }

    return _ringsCompleted;
  }

  public void RemoveFinishedRing() => _ringsCompleted--;

}
