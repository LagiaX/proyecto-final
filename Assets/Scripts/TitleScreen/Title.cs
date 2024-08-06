using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

  public CanvasRenderer pressAnyButton;

  private RectTransform titleRectTransform;
  private float speed = 150f;
  private Vector3 direction;
  private bool titleSet = false;

  void Awake() {
    titleRectTransform = GetComponent<RectTransform>();
  }

  void Start() {
    this.gameObject.SetActive(true);
    titleRectTransform.anchoredPosition3D = AppConfig.titleAnimationFrom;
    direction = (AppConfig.titleAnimationTo - AppConfig.titleAnimationFrom).normalized;
  }

  void Update() {
    TitleAnimation();
    if (pressAnyButton != null && !pressAnyButton.gameObject.activeInHierarchy) {
      pressAnyButton.gameObject.SetActive(titleSet);
    }
  }

  private void TitleAnimation() {
    if (titleRectTransform.anchoredPosition3D.y < AppConfig.titleAnimationTo.y) {
      Vector3 temp = titleRectTransform.anchoredPosition3D;
      temp += direction * speed * Time.deltaTime;
      titleRectTransform.anchoredPosition3D = temp;
    }
    else {
      titleSet = true;
    }
  }
}
