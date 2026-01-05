using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour { 
  public int width;
  public int height;
  public float cellSize;

  private Puyo[,] _grid;
  public static GridManager Instance;
  public GridSnapper snapper;

  private void Awake() {
    Instance = this;
    _grid = new Puyo[width, height];
  }

  public bool IsValid(int x, int y) {
    return x >= 0 && x < width && y >= 0 && y < height;
  }

  public Vector3 GridToWorld(int x, int y) {
    return transform.position + new Vector3(
      x * cellSize,
      y * cellSize,
      0f
    );
  }

  public bool IsCellEmpty(int x, int y) {
    return IsValid(x, y) && _grid[x, y];
  }

  public void SetCell(int x, int y, Puyo puyo) {
    if (!IsValid(x, y)) return;

    _grid[x, y] = puyo;
    puyo.SetGridPosition(x, y);
    puyo.transform.position = GridToWorld(x, y);
  }

  public Puyo GetCell(int x, int y) {
    return IsValid(x, y) ? _grid[x, y] : null;
  }

  public void ClearCell(int x, int y) {
    if (IsValid(x, y))
      _grid[x, y] = null;
  }
}