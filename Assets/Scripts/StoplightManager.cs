using System;
using System.Linq;
using UnityEngine;

public class StoplightManager : MonoBehaviour {
  public TrafficLight[] trafficLights;
  public SerializableDictionary<TrafficColor, Color> colorDict;

  public TrafficLight GetNewStoplight(Vector3 pos) => trafficLights
    .OrderBy(x => Vector3.Distance(x.transform.position, pos))
    .FirstOrDefault();
  
  public int GetIndex(TrafficLight trafficLight) => trafficLights.GetIndex(trafficLight);
  
  public static StoplightManager Instance;

  public void Awake() {
    Instance = this;
  }

  public void Start() {
    
  }
}
