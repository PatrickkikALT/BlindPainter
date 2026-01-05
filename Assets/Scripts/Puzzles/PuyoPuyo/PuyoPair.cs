using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class PuyoPair : MonoBehaviour {
  public float fallSpeed = 0.35f;
  public float rotateStep = 90f;

  public event Action OnLocked;

  public Puyo[] childPuyos;
  
  private Rigidbody rb;
  private bool isHeld;
  private bool isLocked;

  private void Awake() {
    rb = GetComponent<Rigidbody>();
    rb.useGravity = false;
    rb.isKinematic = true;
  }

  private void Update() {
    if (isLocked) return;

    if (!isHeld)
      transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
  }

  public void RotatePair(bool clockwise) {
    if (isLocked) return;

    float angle = clockwise ? -rotateStep : rotateStep;
    transform.Rotate(Vector3.forward, angle, Space.World);
  }

  private void OnTriggerEnter(Collider other) {
    if (isLocked) return;

    if (other.CompareTag("Ground") || other.CompareTag("Puyo")) {
      Lock();
    }
  }

  private void Lock() {
    isLocked = true;
    OnLocked?.Invoke();
  }

  public void ToggleHeld(bool held) {
    isHeld = held;
  }
}