using UnityEngine;

public class Puyo : MonoBehaviour {
  public PuyoColor color;

  [HideInInspector] public int x;
  [HideInInspector] public int y;

  public void SetGridPosition(int gx, int gy) {
    x = gx;
    y = gy;
  }
}