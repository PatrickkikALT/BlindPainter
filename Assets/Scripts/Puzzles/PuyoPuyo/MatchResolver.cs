using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchResolver : MonoBehaviour {
  public GridManager grid;

  private void Start() {
    grid = GridManager.Instance;
  }

  public void Resolve() {
    bool[,] visited = new bool[grid.width, grid.height];

    for (int x = 0; x < grid.width; x++) {
      for (int y = 0; y < grid.height; y++) {
        if (visited[x, y]) continue;

        Puyo start = grid.GetCell(x, y);
        if (!start) continue;

        List<Puyo> cluster = new();
        FloodFill(x, y, start.color, visited, cluster);

        if (cluster.Count >= 4) {
          foreach (var p in cluster) {
            grid.ClearCell(p.x, p.y);
            Destroy(p.gameObject);
          }
        }
      }
    }
  }

  private void FloodFill(int x, int y, PuyoColor color, bool[,] visited, List<Puyo> result) {
    if (!grid.IsValid(x, y) || visited[x, y]) return;

    Puyo p = grid.GetCell(x, y);
    if (!p || p.color != color) return;

    visited[x, y] = true;
    result.Add(p);

    FloodFill(x + 1, y, color, visited, result);
    FloodFill(x - 1, y, color, visited, result);
    FloodFill(x, y + 1, color, visited, result);
    FloodFill(x, y - 1, color, visited, result);
  }
}