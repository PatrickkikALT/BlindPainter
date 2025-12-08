using UnityEngine;

public class PaintBucket : MonoBehaviour {
  [SerializeField] private Color color;
  public Color GetColor() => color;
}
