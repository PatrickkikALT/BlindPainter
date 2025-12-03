using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour {
  [HideInInspector] public bool isAtStoplight;
  private Color _currentStoplightColor;
  [HideInInspector] public TrafficLight currentTrafficLight;
  [Range(0, 100)]
  public int speed;

  private float _currentSpeed;
  private void Start() {
    GetNewLight();
  }

  public void FixedUpdate() {
    _currentStoplightColor = currentTrafficLight.currentColor;
    if (isAtStoplight && _currentStoplightColor != Color.red || !isAtStoplight) {
      _currentSpeed = Mathf.Lerp(_currentSpeed, speed, Time.fixedDeltaTime);
    }
    else {
      _currentSpeed = Mathf.Lerp(_currentSpeed, 0, (speed / 4) * Time.deltaTime);
      
    }
    transform.Translate(transform.forward * (_currentSpeed * Time.fixedDeltaTime));
  }

  private void GetNewLight() {
    currentTrafficLight = StoplightManager.Instance.GetNewStoplight(transform.position);
  }
}
