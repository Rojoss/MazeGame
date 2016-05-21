using UnityEngine;

public class Finish : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        if (Game.Instance.GetState() == GameState.STARTED) {
            Game.Instance.EndGame(true);
        }
    }
}