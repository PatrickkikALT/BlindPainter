using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ColorCoreSocket : MonoBehaviour {
  public ColorCore core;
  
  public void OnInsert(SelectEnterEventArgs args) {
    if (args.interactableObject.transform.TryGetComponent(out ColorSymbol symbol)) {
      core.AddColor(symbol.colorType);
    }
  }

  public void OnRemove(SelectExitEventArgs args) {
    if (args.interactableObject.transform.TryGetComponent(out ColorSymbol symbol)) {
      core.RemoveColor(symbol.colorType);
    }
  }
}