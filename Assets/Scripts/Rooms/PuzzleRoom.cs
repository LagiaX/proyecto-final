using UnityEngine;

public class PuzzleRoom : MonoBehaviour {
  public delegate void RoomResolved(int id);
  public static event RoomResolved Resolved;

  public int id;
  public bool roomClear;
  public RemoteDoor door;
  public MatchPattern matchPattern;

  void OnEnable() {
    matchPattern.PuzzleSolved += OnPuzzleSolved;
  }

  void OnDisable() {
    matchPattern.PuzzleSolved -= OnPuzzleSolved;
  }

  public void OnPuzzleSolved(bool isSolved) {
    roomClear = isSolved;
    if (roomClear) {
      door.OnActivate();
      Resolved?.Invoke(id);
      DestroyRoomLogic(3);
    }
  }

  public async void DestroyRoomLogic(int delay) {
    matchPattern.PuzzleSolved -= OnPuzzleSolved;
    await GarbageManager.RemoveInTime(gameObject, delay);
  }
}
