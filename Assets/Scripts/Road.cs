using System;
using UnityEngine;

public class Road : MonoBehaviour {
  [SerializeField] private Vector3 position;
  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent(out Car _)) {
      other.transform.position = transform.position + position;
    }
  }

  private void OnDrawGizmos() {
    Gizmos.DrawCube(transform.position + position, Vector3.one);
  }
}
