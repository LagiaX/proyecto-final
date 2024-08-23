using System.Threading.Tasks;
using UnityEngine;

public class ActivateObject : MonoBehaviour {

  [Header("Activate on trigger")]
  public GameObject[] gameobjects;
  public bool activateOnEnter = true;
  public bool activateOnExit = false;
  public bool activateOnce;

  private bool _isDestroying;

  public void OnTriggerEnter(Collider other) {
    if (_isDestroying) return;
    if (other.TryGetComponent(out PlayerSystems player)) {
      if (activateOnEnter) {
        _ActivateAllObjects();
      }
      if (activateOnce) {
        _DestroyPostTrigger();
      }
    }
  }

  public void OnTriggerExit(Collider other) {
    if (_isDestroying) return;
    if (other.TryGetComponent(out PlayerSystems player)) {
      if (activateOnEnter) {
        _ActivateAllObjects();
      }
      if (activateOnce) {
        _DestroyPostTrigger();
      }
    }
  }

  private async void _ActivateAllObjects() {
    for (int i = 0; i < gameobjects.Length; i++) {
      if (!gameobjects[i].TryGetComponent(out IActivatable a)) {
        Utils.MissingComponent(typeof(IActivatable).Name, gameobjects[i].name);
      }
      else {
        a.OnActivate();
      }
      await Task.Delay(10);
    }
  }

  private async void _DestroyPostTrigger() {
    _isDestroying = true;
    await GarbageManager.RemoveInTime(gameObject, 2);
  }
}
