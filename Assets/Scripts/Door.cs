using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour {
  public Quaternion open, closed;
  private bool _isOpen;
  private Coroutine _currentRoutine;
  

  public void Open() {
    _isOpen = !_isOpen;
    if (_currentRoutine != null) {
      StopCoroutine(_currentRoutine);
    }
    _currentRoutine = StartCoroutine(OpenDoor());
  }

  public IEnumerator OpenDoor() {
    var target = _isOpen ? open : closed;
    while (Quaternion.Angle(transform.rotation, target) > 1f) {
      transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
      yield return null;
    }
    transform.rotation = target;
  }

}