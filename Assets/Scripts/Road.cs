using System;
using UnityEngine;

public class Road : MonoBehaviour {
  [SerializeField] private Vector3 position;
  [SerializeField] private Quaternion rotation;
  

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent(out Car _)) {
      Matrix4x4 matrix = Matrix4x4.TRS(
        transform.position, 
        rotation. eulerAngles != Vector3.zero ? rotation :  Quaternion.identity, 
        Vector3.one
      );
      
      Vector3 worldPosition = matrix.MultiplyPoint3x4(position);
      
      other.transform.position = worldPosition;
    }
  }

  
  private void OnDrawGizmos() {
    Matrix4x4 matrix;
    matrix = Matrix4x4.TRS(transform.position, rotation.eulerAngles != Vector3.zero ? rotation : Quaternion.identity, Vector3.one);
    Gizmos.matrix = matrix;
    Gizmos.DrawCube(position, Vector3.one);
  }
}
