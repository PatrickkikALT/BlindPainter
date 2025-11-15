using System;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Colorblind", menuName = "ScriptableObjects/Colorblind")]
[Serializable]
public class Colorblind : ScriptableObject {
  public float3x3 colorMatrix;
  public string typeName;
}
