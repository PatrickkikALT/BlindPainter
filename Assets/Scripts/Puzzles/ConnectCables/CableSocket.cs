using UnityEngine;

public class CableSocket : MonoBehaviour {
  public string acceptedCableID;
  public bool isOccupied;

  private void OnTriggerEnter(Collider other) {
    if (isOccupied) return;
    
    if (other.TryGetComponent(out CableEnd cable) && cable.cableID == acceptedCableID) {
      ConnectCable(cable);
    }
  }

  private void ConnectCable(CableEnd cable) {
    isOccupied = true;
    cable.LockToSocket(transform);
    PuzzleManager.Instance.RegisterConnection();
  }
}