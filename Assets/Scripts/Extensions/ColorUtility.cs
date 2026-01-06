using UnityEngine;

public static class ColorUtility {
  public static Color ToRGB(bool r, bool y, bool b) {
    if (r && !y && !b) return new Color(1f, 0f, 0f);
    if (!r && y && !b) return new Color(1f, 0.92f, 0.016f);
    if (!r && !y && b) return new Color(0.16f, 0.32f, 1f);

    if (r && y && !b) return new Color(1f, 0.5f, 0f);
    if (!r && y && b) return new Color(0f, 0.7f, 0.3f);
    if (r && !y && b) return new Color(0.6f, 0.2f, 0.7f);

    if (r && y && b) return new Color(0.4f, 0.26f, 0.13f);

    return Color.black;
  }
}