using UnityEngine;

public class PuzzleRoom : MonoBehaviour {
  public bool roomClear;
  public RemoteDoor door;
  public MatchPattern matchPattern;

  void Awake() {
    if (roomClear) {
      DestroyRoomLogic(3);
      return;
    }
    matchPattern.PuzzleSolved += OnPuzzleSolved;
  }

  void Update() {
    if (roomClear) {
      DestroyRoomLogic(3);
      return;
    }
  }

  public void OnPuzzleSolved(bool isSolved) {
    roomClear = isSolved;
    if (roomClear) {
      door.OnActivate();
    }
  }

  public async void DestroyRoomLogic(int delay) {
    matchPattern.PuzzleSolved -= OnPuzzleSolved;
    await GarbageManager.RemoveInTime(gameObject, delay);
  }
}
