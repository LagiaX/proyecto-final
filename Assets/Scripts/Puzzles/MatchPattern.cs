using UnityEngine;

public class MatchPattern : MonoBehaviour {
  public delegate void isPuzzleSolved(bool isSolved);
  public event isPuzzleSolved PuzzleSolved;

  public int nElements = 3;
  public bool colorMatch;
  public Color[] colorSolution;
  public ColorChanger[] objects;
  public ActivatableTotem[] activatableTotem;
  public bool[] matchingElements;

  private Renderer[] _renderers;

  void Awake() {
    matchingElements = new bool[nElements];
    _renderers = new Renderer[nElements];
    for (int i = 0; i < objects.Length; i++) {
      objects[i].ColorChange += CheckSolved;
    }
  }

  void Start() {
    if (colorMatch) {
      for (int i = 0; i < objects.Length; i++) {
        if (!objects[i].TryGetComponent(out _renderers[i])) {
          Utils.MissingComponent(typeof(Renderer).Name, objects[i].name);
        }
      }
    }
  }

  public void CheckSolved(Color c) {
    bool solved = true;
    int index = 0;
    while (index < objects.Length && solved) {
      matchingElements[index] = colorSolution[index] == _renderers[index].material.color;
      if (matchingElements[index]) {
        activatableTotem[index].OnActivate();
      }
      else {
        activatableTotem[index].Deactivate();
      }
      solved = solved && matchingElements[index];
      index++;
    }
    if (solved) {
      for (int i = 0; i < objects.Length; i++) {
        objects[i].CanChangeColor(false);
      }
    }
    PuzzleSolved?.Invoke(solved);
  }
}
