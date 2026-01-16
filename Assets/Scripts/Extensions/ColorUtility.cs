using UnityEngine;

public static class ColorUtility {
  public static Color ToRGB(bool r, bool y, bool b) {
    //holy shit
    return r switch {
      true when !y && !b => new Color(1f, 0f, 0f),
      false when y && !b => new Color(1f, 0.92f, 0.016f),
      false when !y && b => new Color(0.16f, 0.32f, 1f),
      true when y && !b => new Color(1f, 0.5f, 0f),
      false when y && b => new Color(0f, 0.7f, 0.3f),
      true when !y && b => new Color(0.6f, 0.2f, 0.7f),
      true when y && b => new Color(0.4f, 0.26f, 0.13f),
      _ => Color.black
    };
  }
}