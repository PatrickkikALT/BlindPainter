using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class ExtensionMethods {
  //unity doesnt have an implicit cast from float3 to Vector4 for SetVector so had to make my own
  //because i dont like having to cast it to a Vector3 to then cast it to a Vector4 for the method.
  /// <summary>
  /// Sets a value for a named vector in the material.
  /// </summary>
  /// <param name="name">Property name, e. g. "_WaveAndDistance"</param>
  /// <param name="v">Vector value to set.</param>
  public static void SetVector(this Material material, string name, float3 v) {
    var v4 = new Vector4(v.x, v.y, v.z, 0);
    material.SetVector(name, v4);
  }
  /// <summary>
  /// Returns index of specified object in array
  /// </summary>
  /// <param name="obj">Object to get index from.</param>
  /// <returns></returns>
  public static int GetIndex<T>(this T[] array, T obj) => Array.IndexOf(array, obj);

  /// <summary>
  /// Shuffles queue's contents using the Fisher-Yates shuffle.
  /// <para>This method is O(n).</para>
  /// </summary>
  public static Queue<T> ShuffleQueue<T>(this Queue<T> queue) {
    List<T> list = new List<T>(queue);
    
    //fisher-yates shuffle (O(n))
    for (int i = list.Count - 1; i > 0; i--) {
      int j = UnityEngine.Random.Range(0, i + 1);
      (list[i], list[j]) = (list[j], list[i]);
    }
    
    return new Queue<T>(list);
  }
}
