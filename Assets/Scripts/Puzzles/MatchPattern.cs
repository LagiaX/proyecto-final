using System;
using System.Threading.Tasks;
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
      objects[i].ColorChange += _UpdateMatchingArray;
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

  private void _UpdateMatchingArray(Color c, ColorChanger o) {
    int index = Array.FindIndex(objects, obj => obj == o);
    matchingElements[index] = colorSolution[index] == _renderers[index].material.color;
    if (matchingElements[index]) {
      activatableTotem[index].OnActivate();
    }
    else {
      activatableTotem[index].Deactivate();
    }
    CheckSolved();
  }

  public async void CheckSolved() {
    bool solved = true;
    int index = 0;
    while (index < objects.Length && solved) {
      solved = solved && matchingElements[index];
      index++;
      await Task.Yield();
    }
    if (solved) {
      for (int i = 0; i < objects.Length; i++) {
        objects[i].CanChangeColor(false);
      }
    }
    PuzzleSolved?.Invoke(solved);
  }
}
