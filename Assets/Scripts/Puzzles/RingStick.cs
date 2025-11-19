using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RingStick : MonoBehaviour {
  public SerializableDictionary<Vector3, bool> positions;
  private SerializableKeyValuePair<Vector3, bool> _pair;
  public List<Ring> rings = new();
  public bool completed;
  public int amountToComplete;
  public RingColor typeNeeded;

  public void AddRing(GameObject obj) {
    _pair = positions.entries.First(x => !x.value);
    var valid = _pair.key;
    if (valid.Equals(default)) {
      return;
    }
    
    positions.entries.Find(x => x == _pair).value = true;
    obj.transform.position = transform.position + valid;
    obj.transform.rotation = Quaternion.identity;
    rings.Add(obj.GetComponent<Ring>());

    if (rings.Count > 0) {
      var amount = rings.Count(r => r.color == typeNeeded);
      var wrongAmount = rings.Count(r => r.color != typeNeeded);
      if (amount == amountToComplete && wrongAmount == 0) {
        print("correct");
        PuzzleManager.Instance.FinishRing(this);
      }
    }
    
  }

  public void RemoveRing(GameObject obj) {
    positions.entries.Find(x => x == _pair).value = false;
    rings.Remove(obj.GetComponent<Ring>());
    if (completed) {
      completed = false;
      PuzzleManager.Instance.RemoveFinishedRing();
    }
  }

  public void OnDrawGizmos() {
    foreach (var t in positions.entries) {
      Gizmos.DrawCube(transform.position + t.key, new Vector3(0.05f, 0.02f, 0.05f));
    }
  }
}
