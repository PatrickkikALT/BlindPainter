using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CableEnd : XRGrabInteractable {
  [Header("Cable Settings")] public string cableID;
  public Transform cableRoot;
  public LineRenderer lineRenderer;

  private Vector3 _localStartPoint;
  private bool _isConnected = false;

  protected override void Awake() {
    base.Awake();
    _localStartPoint = cableRoot.position;
    lineRenderer.positionCount = 2;
  }

  protected override void OnSelectEntered(SelectEnterEventArgs args) {
    if (_isConnected) return;
    base.OnSelectEntered(args);
  }

  protected override void OnSelectExited(SelectExitEventArgs args) {
    if (_isConnected) return;
    base.OnSelectExited(args);
  }

  private void FixedUpdate() {
    UpdateCable();
  }

  private void UpdateCable() {
    lineRenderer.SetPosition(0, _localStartPoint);
    lineRenderer.SetPosition(1, transform.position);
  }

  public void LockToSocket(Transform socket) {
    _isConnected = true;
    transform.position = socket.position;
    transform.rotation = socket.rotation;
    transform.SetParent(socket);

    interactionManager.SelectExit(interactorsSelecting[0], this);
    enabled = false;
  }
}