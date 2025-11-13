using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorblindManager : MonoBehaviour {
  public Material colorblindMaterial;
  public Colorblind currentColorblindness;

  [ContextMenu("Update Colorblindness")]
  private void SwitchColorblindness() {
    colorblindMaterial.SetVector("_ColorMatrixR", (Vector3)currentColorblindness.colorMatrix.c0);
    colorblindMaterial.SetVector("_ColorMatrixG", (Vector3)currentColorblindness.colorMatrix.c1);
    colorblindMaterial.SetVector("_ColorMatrixB", (Vector3)currentColorblindness.colorMatrix.c2);
  }
}
