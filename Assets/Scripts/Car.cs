using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour {
  public bool isAtStoplight;
  public Color currentStoplightColor;
  public bool isAlive;
  public TrafficLight currentTrafficLight;

  void Start() {
    isAlive = true;
    GetNewLight();
    StartCoroutine(CheckStoplightDistance());
  }
  public void FixedUpdate() {
    currentStoplightColor = currentTrafficLight.currentColor;
    if (isAtStoplight && currentStoplightColor == Color.green || !isAtStoplight) {
      transform.Translate(transform.forward * Time.fixedDeltaTime);
    }
  }

  public void GetNewLight() {
    currentTrafficLight = StoplightManager.Instance.GetNewStoplight(transform.position);
  }

  public IEnumerator CheckStoplightDistance() {
    while (isAlive) {
      while (!isAtStoplight) {
        yield return new WaitForEndOfFrame();
        isAtStoplight = StoplightManager.Instance.IsCloseToStoplight(transform.position, StoplightManager.Instance.GetIndex(currentTrafficLight));
      }
    }
  }

  public void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("StopLight")) {
      isAtStoplight = false;
    }
  }
}
