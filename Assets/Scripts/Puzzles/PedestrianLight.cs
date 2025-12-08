using System.Collections;
using System.Linq;
using UnityEngine;

public class PedestrianLight : MonoBehaviour {
  public Renderer renderer;
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
      SetColor(TrafficColor.GREEN);
      yield return new WaitForSeconds(red);
      SetColor(TrafficColor.RED);
      yield return new WaitForSeconds(green);
      SetColor(TrafficColor.ORANGE);
      yield return new WaitForSeconds(orange);
    }
  }

  public void SetColor(TrafficColor trafficColor) {
    var color = _colorDict.entries[(int)trafficColor].value;
    
    renderer.materials[(int)trafficColor].SetColor("_BaseColor", color);
    currentColor = color;
    
    //disable other ones
    var toTurnOff = _colorDict.entries.Where(x => x.value != color && _colorDict.entries.IndexOf(x) != 0);
    foreach (var item in toTurnOff) {
      renderer.materials[(int)item.key].SetColor("_BaseColor", Color.black);
    }
  }

}
