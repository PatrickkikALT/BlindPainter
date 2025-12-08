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
  public void Start() {
    var i = PlayerPrefs.GetInt("Colorblindness");
    print(i);
    currentColorblindness = possibleColorblindness[i];
    SwitchColorblindness();
  }

  [ContextMenu("Update Colorblindness")]
  private void SwitchColorblindness(bool setPrefs = false) {
    colorblindMaterial.SetVector("_ColorMatrixR", currentColorblindness.colorMatrix.c0);
    colorblindMaterial.SetVector("_ColorMatrixG", currentColorblindness.colorMatrix.c1);
    colorblindMaterial.SetVector("_ColorMatrixB", currentColorblindness.colorMatrix.c2);
    if (setPrefs) {
      PlayerPrefs.SetInt("Colorblindness", possibleColorblindness.GetIndex(currentColorblindness));
    }
  }

  public void SetColorblindnessWithDropdown(TMP_Dropdown op) {
    var option = op.options[op.value];
    currentColorblindness = possibleColorblindness.First(c => c.typeName == option.text);
    SwitchColorblindness(true);
  }

  public string[] GetOptions() => possibleColorblindness.Select(option => option.typeName).ToArray();

  public void OnValidate() {
    SwitchColorblindness();
  }
}
