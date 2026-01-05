using UnityEngine;

public class GridSnapper : MonoBehaviour {
  public GridManager grid;
  public MatchResolver matcher;

  public void SnapAndLock(PuyoPair pair) {
    foreach (Puyo puyo in pair.childPuyos) {
      Vector3 local = puyo.transform.position - grid.transform.position;

      int x = Mathf.RoundToInt(local.x / grid.cellSize);
      int y = Mathf.RoundToInt(local.y / grid.cellSize);

      y = Mathf.Clamp(y, 0, GridManager.Instance.height - 1);

      if (grid.IsCellEmpty(x, y)) {
        grid.SetCell(x, y, puyo);
      }
    }
    matcher.Resolve();
  }
}