using System.Threading.Tasks;
using UnityEngine;

public class RemoteDoor : MonoBehaviour, IActivatable {

  public Vector3[] positions;
  public float movementSpeed;

  private int _currentPosition = 0;

  private async void _MoveDoor() {
    while (Vector3.Distance(transform.localPosition, positions[_currentPosition]) > 0.01f) {
      Vector3 position = transform.localPosition;
      float scaler = Time.deltaTime;
      await Task.Run(() => {
        position = Vector3.MoveTowards(
          position,
          positions[_currentPosition],
          movementSpeed * scaler
        );
      });
      transform.localPosition = position;
    }
  }

  public void OnActivate() {
    _currentPosition = (_currentPosition + 1) % positions.Length;
    _MoveDoor();
  }
}
