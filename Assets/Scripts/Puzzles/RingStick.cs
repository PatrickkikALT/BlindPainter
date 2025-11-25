using System;
using System.Collections;
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
    StartCoroutine(LerpToPos(obj.transform, transform.position + _pair.key, 0.01f));
    obj.transform.rotation = Quaternion.identity;
    rings.Add(obj.GetComponent<Ring>());
    Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>(), true);
    if (rings.Count > 0) {
      var amount = rings.Count(r => r.color == typeNeeded);
      var wrongAmount = rings.Count(r => r.color != typeNeeded);
      if (amount == amountToComplete && wrongAmount == 0) {
        print("correct");
        PuzzleManager.Instance.FinishRing(this);
      }
    }
    
  }

  public IEnumerator LerpToPos(Transform transform, Vector3 to, float tolerance) {
    var rb = transform.GetComponent<Rigidbody>();
    rb.isKinematic = true;
    while (Vector3.Distance(transform.position, to) > tolerance) {
      transform.position = Vector3.Slerp(transform.position, to, 2 * Time.deltaTime);
      yield return null;
    }

    rb.isKinematic = false;
  }


  public void RemoveRing(GameObject obj) {
    Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>(), false);
    positions.entries.Find(x => x == _pair).value = false;
    rings.Remove(obj.GetComponent<Ring>());
    if (completed) {
      completed = false;
      PuzzleManager.Instance.RemoveFinishedRing();
    }
  }

  public void SetRingPosAfterFinish() {
    for (int i = 0; i < rings.Count; i++) {
      rings[i].transform.position = transform.position + positions.entries[i].key;
      rings[i].transform.rotation = Quaternion.identity;
    }
  }

  public void OnDrawGizmos() {
    foreach (var t in positions.entries) {
      Gizmos.DrawCube(transform.position + t.key, new Vector3(0.05f, 0.02f, 0.05f));
    }
  }
}
