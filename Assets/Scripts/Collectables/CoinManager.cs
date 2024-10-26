using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinManager : MonoBehaviour {
  public static CoinManager instance;

  public GameObject coinPrefab;
  public Vector3[] positions;
  public CoinInfo[] coinInfo;
  public List<GameObject> coins;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    Coin.Collected += _MarkAsCollected;
  }

  void OnDisable() {
    Coin.Collected -= _MarkAsCollected;
  }

  private void _MarkAsCollected(GameObject g) {
    int index = Array.FindIndex(coinInfo, ci => ci.name == g.name);
    coinInfo[index].collected = true;
  }

  public void InitCoin(int index) {
    coinInfo[index] = new CoinInfo();
    coinInfo[index].name = "Coin_" + index;
    coinInfo[index].position = positions[index];
    coinInfo[index].collected = false;
  }

  public void GenerateCoin(int index) {
    GameObject coin = Instantiate(coinPrefab, transform);
    coins.Add(coin);
    coin.name = coinInfo[index].name;
    coin.transform.localPosition = coinInfo[index].position;
  }
}
