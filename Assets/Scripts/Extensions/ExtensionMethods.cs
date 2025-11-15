using Unity.Mathematics;
using UnityEngine;

public static class ExtensionMethods {
  //unity doesnt have an implicit cast from float3 to Vector4 for SetVector so had to make my own
  //because i dont like having to cast it to a Vector3 to then cast it to a Vector4 for the method.
  public static void SetVector(this Material material, string name, float3 v) {
    var v4 = new Vector4(v.x, v.y, v.z, 0);
    material.SetVector(name, v4);
  }
  
}
