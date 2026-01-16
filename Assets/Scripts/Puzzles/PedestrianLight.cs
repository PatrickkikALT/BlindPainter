using System.Collections;
using System.Linq;
using UnityEngine;

public class PedestrianLight : TrafficLight {
  //disable car functionality
  public override void OnTriggerExit(Collider other) {}
  public override void OnTriggerEnter(Collider other) {}
  
  public override IEnumerator LightPattern() {
    while (running) {
      SetColor(TrafficColor.GREEN);
      yield return new WaitForSeconds(green);
      SetColor(TrafficColor.ORANGE);
      yield return new WaitForSeconds(orange);
      SetColor(TrafficColor.RED);
      yield return new WaitForSeconds(red);
    }
  }
}
