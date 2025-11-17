using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Puzzle", menuName = "Scriptable Objects/Puzzle")]
public class Puzzle : ScriptableObject {
  public Colorblind[] allowedColorblindness;
  public int id;
  public string puzzleName;
  public GameObject puzzlePrefab;
}
