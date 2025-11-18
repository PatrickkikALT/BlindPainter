using System;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;

[Serializable]
public struct Symbol : IEquatable<Symbol> {
  public Sprite sprite;
  public Color color;

  public bool Equals(Symbol other) {
    return Equals(sprite, other.sprite) && color.Equals(other.color);
  }

  public override bool Equals(object obj) {
    return obj is Symbol other && Equals(other);
  }

  public override int GetHashCode() {
    return HashCode.Combine(sprite, color);
  }
}

public class SymbolColor : MonoBehaviour {
  private Symbol[] _symbols;
  public SpriteRenderer spriteRenderer;
  public void Start() {
    var c = GetComponent<Renderer>().material.color;
    _symbols = GameManager.Instance.symbols;
    var symbol = _symbols
      .Where(x => AreColorsSimilar(c, x.color, GameManager.Instance.symbolTolerance))
      .OrderBy(x => ColorDifference(c, x.color))
      .FirstOrDefault();

    spriteRenderer.sprite = symbol.sprite;
  }
  
  Symbol FindClosestSymbol(Color target, float tolerance, int maxRetries = 3, float increment = 0.05f, int attempt = 0)
  {
    Symbol symbol = _symbols
      .Where(x => AreColorsSimilar(target, x.color, tolerance))
      .OrderBy(x => ColorDifference(target, x.color))
      .FirstOrDefault();

    if (symbol.Equals(default(Symbol)) || attempt >= maxRetries)
      return symbol; 

    return FindClosestSymbol(target, tolerance + increment, maxRetries, increment, attempt + 1);
  }


  public bool AreColorsSimilar(Color c1, Color c2, float tolerance) {
    return Math.Abs(c1.r - c2.r) < tolerance &&
           Math.Abs(c1.g - c2.g) < tolerance &&
           Math.Abs(c1.b - c2.b) < tolerance;
  }

  public float ColorDifference(Color c1, Color c2) {
    return Mathf.Abs(c1.r - c2.r) +
           Mathf.Abs(c1.g - c2.g) +
           Mathf.Abs(c1.b - c2.b);
  }

}