using TMPro;
using UnityEngine;

public class AssignKey : MonoBehaviour {

  public TMP_Text text;
  KeyCode key;

  void OnGUI() {
    Event e = Event.current;
    if (e.type == EventType.KeyDown) {
      if (e.keyCode == KeyCode.None) return;
      e.Use();
      _AssignKey(e.keyCode);
    }
  }

  public void _AssignKey(KeyCode keyCode) {
    key = keyCode;
    text.text = key.ToString();
  }
}
