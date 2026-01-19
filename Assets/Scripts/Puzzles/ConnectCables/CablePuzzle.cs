using System;
using System.Collections.Generic;
using UnityEngine;

public class CablePuzzle : MonoBehaviour {
  public ParticleSystem cableConfetti;
  public int totalConnectionsRequired;
  private int _currentConnections;
  public Transform[] sockets;
  public Transform[] cables;
  public float[] yPositions;
  private Queue<float> possibleYPositions;
  public AudioSource confettiAudio;

  private void Start() {
    Vector3 basePosition = transform.position;

    // randomize sockets
    Queue<float> socketYPositions = new Queue<float>(yPositions);
    socketYPositions = socketYPositions.RandomizeQueue();
    foreach (var socket in sockets) {
      socket.localPosition = Vector3.up * socketYPositions.Dequeue();
    }
    // randomize cables
    Queue<float> cableYPositions = new Queue<float>(yPositions);
    cableYPositions = cableYPositions.RandomizeQueue();
    foreach (var cable in cables) {
      cable.localPosition = Vector3.up * cableYPositions.Dequeue();
    }
  }

  public void RegisterConnection() {
    _currentConnections++;

    if (_currentConnections >= totalConnectionsRequired) {
      CompletePuzzle();
    }
  }

  private void CompletePuzzle() { 
    confettiAudio.Play();
    cableConfetti.Play();
  }
}