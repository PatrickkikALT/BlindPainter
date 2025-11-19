using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ColorblindDropdown : MonoBehaviour {
  private void Start() {
    var list = ColorblindManager.Instance.GetOptions().Select(s => new TMP_Dropdown.OptionData(s)).ToList();
    var dropdown = GetComponent<TMP_Dropdown>();
    dropdown.options = list;
    
  }
}
