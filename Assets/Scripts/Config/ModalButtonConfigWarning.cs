using UnityEngine;
using UnityEngine.EventSystems;

public class ModalButtonConfigWarning : MonoBehaviour {

  void OnEnable() {
    EventSystem.current.SetSelectedGameObject(gameObject);
  }

  void OnGUI() {
    Event e = Event.current;
    if (e.type == EventType.KeyDown) {
      gameObject.SetActive(false);
    }
  }
}
