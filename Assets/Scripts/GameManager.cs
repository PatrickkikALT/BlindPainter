using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public Symbol[] symbols;
  public static GameManager Instance;
  public float symbolTolerance;
  private void Start() {
    Instance = this;
  }
}
