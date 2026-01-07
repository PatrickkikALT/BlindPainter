using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CableEnd : XRGrabInteractable {
  [Header("Cable Settings")] public string cableID;
  public Transform cableRoot;
  public LineRenderer lineRenderer;
  
  public bool isConnected;
  
  private void FixedUpdate() {
    UpdateCable();
  }


  private void UpdateCable() {
    lineRenderer.SetPosition(0, cableRoot.position);
    lineRenderer.SetPosition(1, transform.position);
  } 

  public void LockToSocket(Transform socket, bool correct) {
    
    //allow interaction manager to cleanly deselect the interactable
    interactionManager.CancelInteractableSelection((IXRSelectInteractable)this);
    //disallow player from interacting with cable again if connected correctly
    if (correct) {
      isConnected = true;
      interactionLayers = InteractionLayerMask.GetMask("None");
    }
    
    transform.SetParent(socket, worldPositionStays: false);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
  }
}