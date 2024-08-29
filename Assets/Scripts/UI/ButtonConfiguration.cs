using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static AppConfig;

public class ButtonConfiguration : MonoBehaviour {

  public Dictionary<Control, TMP_Text> controls = new Dictionary<Control, TMP_Text>();
  public TMP_Text moveUp;
  public TMP_Text moveRight;
  public TMP_Text moveDown;
  public TMP_Text moveLeft;
  public TMP_Text jump;
  public TMP_Text shoot;
  public TMP_Text changeWeapon;
  public TMP_Text lockTarget;

  private Control _currentControl;
  private bool _isCapturingKey;

  void Awake() {
    // TODO: Load config from cfg file
    controls[Control.MoveUp] = moveUp;
    controls[Control.MoveRight] = moveRight;
    controls[Control.MoveDown] = moveDown;
    controls[Control.MoveLeft] = moveLeft;
    controls[Control.Jump] = jump;
    controls[Control.Shoot] = shoot;
    controls[Control.ChangeWeapon] = changeWeapon;
    controls[Control.LockTarget] = lockTarget;
  }

  void Start() {
    _LoadSavedValues();
  }

  void OnGUI() {
    if (_isCapturingKey) {
      Event e = Event.current;
      if (e.type == EventType.KeyDown) {
        if (e.keyCode == KeyCode.None) return;
        e.Use();
        _AssignKey(e.keyCode);
        _isCapturingKey = false;
      }
    }
  }

  private void _AssignKey(KeyCode keyCode) {
    KeyBindings[_currentControl] = keyCode;
    controls[_currentControl].text = keyCode.ToString();
  }

  private void _LoadSavedValues() {
    foreach (string str in Enum.GetNames(typeof(Control))) {
      Control currentControl = Enum.Parse<Control>(str);
      controls[currentControl].text = KeyBindings[currentControl].ToString();
    }
  }

  public void OnControlSelect(int control) {
    _currentControl = (Control)control;
    _isCapturingKey = true;
  }
}
