using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CableSocket : XRSocketInteractor {
  [Header("Cable Settings")] 
  public string acceptedCableID;
  public bool isOccupied;
  
  public void OnCableInserted(SelectEnterEventArgs args) {
    if (isOccupied) return;
    if (args.interactableObject.transform.TryGetComponent<CableEnd>(out var cable)) {
      if (cable.isConnected) return;
      bool correct = cable.cableID == acceptedCableID;

      cable.LockToSocket(attachTransform, correct);
      PuzzleManager.Instance.RegisterConnection();
      isOccupied = true;
    }
  }
}