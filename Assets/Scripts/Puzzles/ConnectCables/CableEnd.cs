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
  private Vector3 _originalScale;

  public AudioSource connectedAudio;
  public AudioSource disconnectedAudio;
  public ParticleSystem lightningParticle;
  
  private void FixedUpdate() {
    if (!isConnected) {
      UpdateCable();
    }
  }

  protected override void Grab() {
    disconnectedAudio.Play();
    //we do need to check if it exists here since if the correct cable is inserted, the particle system gets destroyed to prevent
    //the particles playing after winning for some reason?
    if (lightningParticle) {
      lightningParticle.Play();
      lightningParticle.loop = true;
    }
  }

  protected override void Drop() {
    disconnectedAudio.Stop();
    if (lightningParticle) {
      lightningParticle.Stop();
      lightningParticle.loop = false;
    }
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
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    lineRenderer.SetPosition(1, socket.position);
  }
}