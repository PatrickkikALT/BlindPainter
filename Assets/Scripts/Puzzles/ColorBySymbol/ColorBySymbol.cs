using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorBySymbol : MonoBehaviour {
  [SerializeField] private Color[] colors;
  [SerializeField] private List<PaintBucket> buckets;
  [SerializeField] private GameObject bucketPrefab;
  [SerializeField] private ParticleSystem confetti;
  private SerializableDictionary<PaintSlot, bool> _completedSlots;

  private void Start() {
    foreach (var color in colors) {
      var paintBucket = Instantiate(bucketPrefab, transform.position + 
                                                  GetPosition(colors.GetIndex(color)), 
                                                  Quaternion.identity).GetComponent<PaintBucket>();
      paintBucket.Color = color;
      buckets.Add(paintBucket);
    }
  }

  public void CompleteSlot(PaintSlot slot) {
    _completedSlots.entries.Find(x => x.key == slot).value = true;
    
    if (_completedSlots.entries.Count(x => x.value == true) == _completedSlots.entries.Count) {
      Complete();
    }
  }
  
  private void Complete() {
    confetti.Play();
  }
  
  private void OnDrawGizmos() {
    if (colors.Length <= 0) return;
    for (int i = 0; i < colors.Length; i++) {
      var pos = GetPosition(i);
      Gizmos.color = colors[i];
      Gizmos.DrawCube(transform.position + pos, Vector3.one * 0.2f);
      
    }
  }

  private Vector3 GetPosition(int i) {
    int col = i % 4;
    int row = i / 4;

    float x = -0.3f + (col * 0.3f);
    float z = 0.2f + (row * 0.3f);

    return new Vector3(x, -transform.position.y * 0.80f, z);
  }

}
