using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TrafficColor {
  RED = 0,
  ORANGE = 1,
  GREEN = 2,
}

public class TrafficLight : MonoBehaviour {
  public Material[] lightMaterial;
  public Color currentColor;
  public bool running;

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

  public void OnTriggerEnter(Collider other) {
    print("Collision with " + other.name);
    if (other.TryGetComponent(out Car car)) {
      car.isAtStoplight = true;
    }
  }

  public void OnTriggerExit(Collider other) {
    print("Collision with " + other.name + " stopped");
    if (other.TryGetComponent(out Car car)) {
      car.isAtStoplight = false;
      car.currentTrafficLight = this;
    }
  }

  public void SetColor(TrafficColor trafficColor) {
    var color = _colorDict.entries[(int)trafficColor].value;
    
    lightMaterial[(int)trafficColor].SetColor("_BaseColor", color);
    currentColor = color;
    
    //disable other ones
    var toTurnOff = _colorDict.entries.Where(x => x.value != color);
    foreach (var item in toTurnOff) {
      lightMaterial[(int)item.key].SetColor("_BaseColor", Color.black);
    }
  }
}
