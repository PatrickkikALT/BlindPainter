using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ColorblindDropdown : MonoBehaviour {
  private TMP_Dropdown _dropdown;
  private void Start() {
    var list = ColorblindManager.Instance.GetOptions().Select(s => new TMP_Dropdown.OptionData(s)).ToList();
    _dropdown = GetComponent<TMP_Dropdown>();
    _dropdown.options = list;
    Invoke(nameof(Init), 0.01f); //lol
  }

  private void Init() {
    _dropdown.value = ColorblindManager.Instance.possibleColorblindness.GetIndex(ColorblindManager.Instance.currentColorblindness);
    _dropdown.RefreshShownValue();
  }
  
}
