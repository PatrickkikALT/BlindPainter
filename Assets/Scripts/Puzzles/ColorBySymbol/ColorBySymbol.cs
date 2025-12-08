using UnityEngine;

public class ColorBySymbol : MonoBehaviour {
  [SerializeField] private Color[] colors;

  private void OnDrawGizmos() {
    var x = 0;
    var z = 0;
    for (int i = 0; i < colors.Length; i++) {
      if (i % 4 == 0) {
        z++;
        x = 0;
      }

      var pos = new Vector3(x, 0, z);
      x++;
      Gizmos.DrawCube(pos, Vector3.one * 0.2f);
      
    }
  }
}
