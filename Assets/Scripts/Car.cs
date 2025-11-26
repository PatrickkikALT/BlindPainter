using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour {
  [HideInInspector] public bool isAtStoplight;
  private Color _currentStoplightColor;
  [HideInInspector] public TrafficLight currentTrafficLight;
  [Range(0, 100)]
  public int speed;
  void Start() {
    GetNewLight();
  }

  public void FixedUpdate() {
    _currentStoplightColor = currentTrafficLight.currentColor;
    if (isAtStoplight && _currentStoplightColor != Color.red || !isAtStoplight) {
      transform.Translate(transform.forward * (speed * Time.fixedDeltaTime));
    }
  }

  public void GetNewLight() {
    currentTrafficLight = StoplightManager.Instance.GetNewStoplight(transform.position);
  }
}
