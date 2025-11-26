using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {
  public Material lightMaterial;
  public Color currentColor;
  public bool running;
  
  public float red, green, orange;
  
  public void Start() {
    StartCoroutine(LightPattern());
  }

  public IEnumerator LightPattern() {
    while (running) {
      SetColor(Color.red);
      yield return new WaitForSeconds(red);
      SetColor(Color.green);
      currentColor = lightMaterial.GetColor("_BaseColor");
      yield return new WaitForSeconds(green);
      SetColor(Color.orange);
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

  public void SetColor(Color color) {
    lightMaterial.SetColor("_BaseColor", color);
    currentColor = color;
  }

}
