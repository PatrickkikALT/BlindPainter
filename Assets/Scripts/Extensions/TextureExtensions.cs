using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Rendering;

public struct AverageColorResult {
  public double r;
  public double g;
  public double b;
  public double a;
  public int count;
}

[BurstCompile]
public struct AverageColorJob : IJob {
  [ReadOnly] public NativeArray<Color32> pixels;
  public NativeArray<AverageColorResult> result;

  public void Execute() {
    double rSum = 0, gSum = 0, bSum = 0, aSum = 0;
    double totalAlpha = 0;

    foreach (var c in pixels) {
      double alpha = c.a / 255.0;

      rSum += c.r * alpha;
      gSum += c.g * alpha;
      bSum += c.b * alpha;
      aSum += alpha;

      totalAlpha += alpha;
    }

    AverageColorResult r = new AverageColorResult();
    r.count = pixels.Length;

    if (totalAlpha > 0) {
      r.r = rSum / totalAlpha;
      r.g = gSum / totalAlpha;
      r.b = bSum / totalAlpha;
      r.a = aSum / pixels.Length;
    }

    result[0] = r;
  }
}

public static class TextureExtensions {
  public static Color GetColorFromPixelData(this Material mat) {
    if (!mat.mainTexture) return mat.color;
    if (mat.mainTexture is not Texture2D t2d) throw new TypeAccessException();
    if (mat.shader.name == "Shader Graphs/Tri-Planar ShaderGraph") {
      t2d = mat.GetTexture("_ColorMap") as Texture2D;
    }
    Color32[] col = t2d.GetPixels32();

    NativeArray<Color32> pixels = new NativeArray<Color32>(col.Length, Allocator.TempJob);
    NativeArray<AverageColorResult> result = new NativeArray<AverageColorResult>(1, Allocator.TempJob);

    for (int i = 0; i < col.Length; i++) {
      pixels[i] = col[i];
    }

    AverageColorJob job = new AverageColorJob {
      pixels = pixels,
      result = result
    };

    job.Schedule().Complete();

    AverageColorResult r = result[0];

    pixels.Dispose();
    result.Dispose();

    if (r.count == 0) return Color.clear;
    var cor = new Color(
      (float)r.r / 255,
      (float)r.g / 255,
      (float)r.b / 255,
      (float)r.a);
    Debug.Log(cor + mat.name);
    return cor;
  }
}