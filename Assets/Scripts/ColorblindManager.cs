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
  public Queue<Colorblind> colorblindQueue = new Queue<Colorblind>();
  public List<Colorblind> colorblindQueueList = new List<Colorblind>();

  private void Awake() {
    Instance = this;
  }
  public void Start() {
    var i = PlayerPrefs.GetInt("Colorblindness");
    SetColorblindness(possibleColorblindness[i]);
    colorblindQueue = new Queue<Colorblind>(colorblindQueueList);
  }

  public void SetColorblindness(Colorblind colorblind) {
    currentColorblindness = colorblind;
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
