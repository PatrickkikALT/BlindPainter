using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Symbol : IEquatable<Symbol> {
  public Sprite sprite;
  public Color color;

  public bool Equals(Symbol other) => Equals(sprite, other.sprite) && color.Equals(other.color);

  public override bool Equals(object obj) => obj is Symbol other && Equals(other);

  public override int GetHashCode() => HashCode.Combine(sprite, color);
}

public class SymbolColor : MonoBehaviour {
  private Symbol[] _symbols;
  public SpriteRenderer spriteRenderer;

  public void Start() {
    var mat = GetComponent<Renderer>().material;
    var c = mat.GetColorFromPixelData();
    _symbols = GameManager.Instance.symbols;
    var symbol = FindClosestSymbol(c);
    spriteRenderer.sprite = symbol.sprite;
  }

  private Symbol FindClosestSymbol(Color target) {
    return _symbols
      .OrderBy(x => DeltaE(target, x.color))
      .FirstOrDefault();
  }
  
  //ugly but no other way because formulas are wack
  //we do this because rgb doesnt account for human perception which is needed for the symbols, as small differences is
  //rgb values can mean a whole different color. so we use the CIELAB color space (https://en.wikipedia.org/wiki/CIELAB_color_space)
  //which better accounts for human perception.
  private Vector3 RGBToLab(Color c) {
    //rgb to linear
    float r = c.r <= 0.04045f ? c.r / 12.92f : Mathf.Pow((c.r + 0.055f) / 1.055f, 2.4f);
    float g = c.g <= 0.04045f ? c.g / 12.92f : Mathf.Pow((c.g + 0.055f) / 1.055f, 2.4f);
    float b = c.b <= 0.04045f ? c.b / 12.92f : Mathf.Pow((c.b + 0.055f) / 1.055f, 2.4f);
    //linear to xyz
    float x = r * 0.4124f + g * 0.3576f + b * 0.1805f;
    float y = r * 0.2126f + g * 0.7152f + b * 0.0722f;
    float z = r * 0.0193f + g * 0.1192f + b * 0.9505f;

    //normalize for d65
    x /= 0.950489f;
    y /= 1.00000f;
    z /= 1.08884f;

    //xyz to LAB
    float fx = x > 0.008856f ? Mathf.Pow(x, 1f / 3f) : (7.787f * x + 16f / 116f);
    float fy = y > 0.008856f ? Mathf.Pow(y, 1f / 3f) : (7.787f * y + 16f / 116f);
    float fz = z > 0.008856f ? Mathf.Pow(z, 1f / 3f) : (7.787f * z + 16f / 116f);

    float l = 116f * fy - 16f;
    float a = 500f * (fx - fy);
    float bLab = 200f * (fy - fz);

    return new Vector3(l, a, bLab);
  }

  private float DeltaE(Color c1, Color c2) {
    Vector3 lab1 = RGBToLab(c1);
    Vector3 lab2 = RGBToLab(c2);
    return Vector3.Distance(lab1, lab2);
  }
}