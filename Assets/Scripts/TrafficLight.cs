using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TrafficColor {
  NONE = 0,
  RED = 1,
  ORANGE = 2,
  GREEN = 3,
}

public class TrafficLight : MonoBehaviour {
  public Renderer renderer;
  public Color currentColor;
  public bool running;
  public int emission;

  private SerializableDictionary<TrafficColor, Color> _colorDict;
  [SerializeField] private float red, green, orange;
  
  public void Start() {
    _colorDict = StoplightManager.Instance.colorDict;
    StartCoroutine(LightPattern());
  }

  public IEnumerator LightPattern() {
    while (running) {
      SetColor(TrafficColor.RED);
      yield return new WaitForSeconds(red);
      SetColor(TrafficColor.GREEN);
      yield return new WaitForSeconds(green);
      SetColor(TrafficColor.ORANGE);
      yield return new WaitForSeconds(orange);
    }
  }

  public virtual void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out Car car)) {
      car.isAtStoplight = true;
    }
  }

  public virtual void OnTriggerExit(Collider other) {
    if (other.TryGetComponent(out Car car)) {
      car.isAtStoplight = false;
      car.currentTrafficLight = this;
    }
  }

  public void SetColor(TrafficColor trafficColor) {
    var color = _colorDict.entries[(int)trafficColor].value;
    
    var material = renderer.materials[(int)trafficColor];
    material.SetColor("_BaseColor", color);
    material.SetColor("_EmissionColor", color * emission);
    currentColor = color;
    
    //disable other ones
    var toTurnOff = _colorDict.entries.Where(x => x.value != color && _colorDict.entries.IndexOf(x) != 0);
    foreach (var item in toTurnOff) {
      var mat2 = renderer.materials[(int)item.key];
      mat2.SetColor("_BaseColor", _colorDict.entries[(int)TrafficColor.NONE].value);
      mat2.SetColor("_EmissionColor", _colorDict.entries[(int)TrafficColor.NONE].value);
    }
  }
}
