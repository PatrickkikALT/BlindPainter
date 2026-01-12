using System.Collections;
using System.Linq;
using UnityEngine;

public class PedestrianLight : TrafficLight {
  //disable car functionality
  public override void OnTriggerExit(Collider other) {}
  public override void OnTriggerEnter(Collider other) {}
}
