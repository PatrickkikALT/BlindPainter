using UnityEngine;

public class PuyoSpawner : MonoBehaviour {
  [Header("References")] public GridManager grid;
  public GameObject puyoPairPrefab;

  [Header("Spawn Settings")] public float spawnHeightOffset = 1.0f;
  public int spawnColumn = 2;

  private PuyoPair _activePair;

  private void Start() {
    SpawnNextPair();
    grid = GridManager.Instance;
  }

  private void SpawnNextPair() {
    Vector3 spawnPos = grid.GridToWorld(spawnColumn, grid.height - 1);
    if (!grid.IsValid((int)spawnPos.x, (int)spawnPos.y)) {
      print("failed");
      return;
    }
    spawnPos.y += spawnHeightOffset;

    GameObject pairObj = Instantiate(puyoPairPrefab, spawnPos, Quaternion.identity);
    _activePair = pairObj.GetComponent<PuyoPair>();

    AssignRandomColors(_activePair);
    _activePair.OnLocked += HandlePairLocked;
  }

  private void AssignRandomColors(PuyoPair pair) {
    Puyo[] puyos = pair.childPuyos;

    foreach (var p in puyos) {
      p.color = (PuyoColor)Random.Range(0, System.Enum.GetValues(typeof(PuyoColor)).Length);
      ApplyColorVisual(p);
    }
  }

  private void ApplyColorVisual(Puyo puyo) {
    Renderer r = puyo.GetComponent<Renderer>();
    r.material.color = puyo.color switch {
      PuyoColor.Red => Color.red,
      PuyoColor.Blue => Color.blue,
      PuyoColor.Green => Color.green,
      PuyoColor.Yellow => Color.yellow,
      PuyoColor.Purple => new Color(0.6f, 0f, 0.8f),
      _ => Color.white
    };
  }

  private void HandlePairLocked() {
    grid.snapper.SnapAndLock(_activePair);
    _activePair = null;
    SpawnNextPair();
  }
}