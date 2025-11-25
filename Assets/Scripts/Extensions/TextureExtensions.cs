using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine;

public struct AverageColorResult {
  public long r;
  public long g;
  public long b;
  public long a;
  public int count;
}

[BurstCompile]
public struct AverageColorJob : IJob
{
  [ReadOnly] public NativeArray<Color32> pixels;
  public NativeArray<AverageColorResult> result;

  public void Execute()
  {
    AverageColorResult r = new AverageColorResult();
    for (int i = 0; i < pixels.Length; i++)
    {
      Color32 c = pixels[i];
      r.r += c.r;
      r.g += c.g;
      r.b += c.b;
      r.a += c.a;
      r.count++;
    }
    result[0] = r;
  }
}


public static class TextureExtensions {
  public static Color GetColorFromPixelData(this Material mat) {
    if (!mat.mainTexture) {
      return mat.color;
    }

    if (mat.mainTexture is not Texture2D t2d) {
      throw new TypeAccessException();
    }

    var col = t2d.GetPixelData<Color32>(1);
    
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

    if (r.count == 0) {
      return new Color32(0, 0, 0, 0);
    }

    return new Color32(
      (byte)(r.r / r.count),
      (byte)(r.g / r.count),
      (byte)(r.b / r.count),
      (byte)(r.a / r.count)
    );
  }
}