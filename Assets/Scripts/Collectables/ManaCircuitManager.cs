using System.Collections.Generic;
using UnityEngine;

public class ManaCircuitManager : MonoBehaviour {
  public static ManaCircuitManager instance;

  public GameObject manaCircuitPrefab;
  public Vector3[] positions;
  public CoinInfo[] circuitInfo;
  public List<GameObject> manaCircuits;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    ManaCircuit.Collected += _MarkAsCollected;
  }

  void OnDisable() {
    ManaCircuit.Collected -= _MarkAsCollected;
  }

  private void _MarkAsCollected(int id) {
    circuitInfo[id].collected = true;
  }

  public void InitCircuit(int index) {
    circuitInfo[index] = new CoinInfo();
    circuitInfo[index].name = "ManaCircuit_" + index;
    circuitInfo[index].position = positions[index];
    circuitInfo[index].collected = false;
  }

  public void GenerateCircuit(int index) {
    GameObject circuit = Instantiate(manaCircuitPrefab, transform);
    manaCircuits.Add(circuit);
    ManaCircuit m;
    if (circuit.TryGetComponent(out m)) {
      m.id = index;
    }
    circuit.name = circuitInfo[index].name;
    circuit.transform.localPosition = circuitInfo[index].position;
  }
}
