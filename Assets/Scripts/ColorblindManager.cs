using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorblindManager : MonoBehaviour {
  public Material colorblindMaterial;
  public Colorblind currentColorblindness;

  [ContextMenu("Update Colorblindness")]
  private void SwitchColorblindness() {
    colorblindMaterial.SetVector("_ColorMatrixR", currentColorblindness.colorMatrix.c0);
    colorblindMaterial.SetVector("_ColorMatrixG", currentColorblindness.colorMatrix.c1);
    colorblindMaterial.SetVector("_ColorMatrixB", currentColorblindness.colorMatrix.c2);
  }

  public void OnValidate() {
    SwitchColorblindness();
  }
}
