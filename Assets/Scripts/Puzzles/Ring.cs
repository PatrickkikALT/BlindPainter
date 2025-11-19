using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public enum RingColor {
  RED,
  BLUE,
  GREEN
}

public class Ring : XRGrabInteractable {
  public Collider col;
  private RingStick _currentStick;
  public Rigidbody rb;
  public RingColor color;
  
  private void Start() {
    rb = GetComponent<Rigidbody>();
    col = GetComponent<Collider>();
  }

  public void OnRelease() {
    Collider[] cols = new Collider[4];
    if (Physics.OverlapBoxNonAlloc(col.bounds.center, col.bounds.extents / 2, cols, Quaternion.identity, ~0, QueryTriggerInteraction.Collide) > 0) {
      foreach (Collider col in cols) {
        if (!col) continue;
        if (col.gameObject.layer == LayerMask.NameToLayer("RingStick")) {
          col.TryGetComponent(out RingStick stick);
          _currentStick = stick;
          stick.AddRing(gameObject);
          break;
        }
      }
    }
  }

  public void OnGrab() {
    if (_currentStick) {
      print("Removed from stick");
      _currentStick.RemoveRing(gameObject);
      _currentStick = null;
    }
  }
}
