using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorblindManager : MonoBehaviour {
  public Material colorblindMaterial;
  public Colorblind currentColorblindness;
  public Colorblind[] possibleColorblindness;
  public static ColorblindManager Instance;

  private void Awake() {
    Instance = this;
  }

  [ContextMenu("Update Colorblindness")]
  private void SwitchColorblindness() {
    colorblindMaterial.SetVector("_ColorMatrixR", currentColorblindness.colorMatrix.c0);
    colorblindMaterial.SetVector("_ColorMatrixG", currentColorblindness.colorMatrix.c1);
    colorblindMaterial.SetVector("_ColorMatrixB", currentColorblindness.colorMatrix.c2);
  }

  public void SetColorblindnessWithDropdown(TMP_Dropdown op) {
    var option = op.options[op.value];
    currentColorblindness = possibleColorblindness.First(c => c.typeName == option.text);
    SwitchColorblindness();
  }

  public string[] GetOptions() => possibleColorblindness.Select(option => option.typeName).ToArray();

  public void OnValidate() {
    SwitchColorblindness();
  }
}
