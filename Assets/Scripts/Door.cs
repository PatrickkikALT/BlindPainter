using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour {
  public Quaternion open, closed;
  private bool _isOpen;
  private Coroutine _currentRoutine;
  private Transform _hinge;

  private void Start() {
    _hinge = transform.parent;
  }

  public void Open() {
    _isOpen = !_isOpen;
    if (_currentRoutine != null) {
      StopCoroutine(_currentRoutine);
    }
    _currentRoutine = StartCoroutine(OpenDoor());
  }

  public IEnumerator OpenDoor() {
    var target = _isOpen ? open : closed;
    while (Quaternion.Angle(_hinge.rotation, target) > 1f) {
      _hinge.rotation = Quaternion.Slerp(_hinge.rotation, target, Time.deltaTime);
      yield return null;
    }
    _hinge.rotation = target;
  }

}