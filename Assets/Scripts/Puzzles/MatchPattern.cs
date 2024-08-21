using UnityEngine;

public class MatchPattern : MonoBehaviour {
  public delegate void isPuzzleSolved(bool isSolved);
  public static event isPuzzleSolved PuzzleSolved;

  public int nElements = 3;
  public bool colorMatch;
  public Color[] colorSolution;
  //public string[] strintSolution;
  //public Vector3[] positionSolution;
  //public Vector3[] rotationSolution;
  public GameObject[] gameobjects;
  public bool[] matchingElements;

  private Renderer[] _renderers;

  void Awake() {
    matchingElements = new bool[nElements];
    _renderers = new Renderer[nElements];
    ColorChanger.ColorChange += CheckSolved;
  }

  void Start() {
    if (colorMatch) {
      for (int i = 0; i < gameobjects.Length; i++) {
        if (!gameobjects[i].TryGetComponent(out _renderers[i])) {
          Utils.MissingComponent(typeof(Renderer).Name, gameobjects[i].name);
        }
      }
    }
  }

  public void CheckSolved(Color c) {
    bool solved = true;
    int index = 0;
    while (index < gameobjects.Length && solved) {
      matchingElements[index] = colorSolution[index] == _renderers[index].material.color;
      solved = solved && matchingElements[index];
      index++;
    }
    PuzzleSolved?.Invoke(solved);
  }
}
