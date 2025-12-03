using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public Symbol[] symbols;
  public static GameManager Instance;

  private void Awake() {
    Instance = this;
  }
  private void Start() {
    Application.targetFrameRate = 60;
  }
}
