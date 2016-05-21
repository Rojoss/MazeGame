using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    private GameState state = GameState.INIT;

    public Fader fader;
    public Timer timer;

    public static Game Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        StartGame();
    }

    public GameState GetState() {
        return state;
    }

    public void StartGame() {
        if (state != GameState.INIT) {
            Debug.LogWarning("Trying to start a game that hasn't been initialized!");
            return;
        }

        state = GameState.STARTED;
        StartCoroutine(OnStart());
    }

    private IEnumerator OnStart() {
        yield return new WaitForSeconds(1f);
        fader.ShowLoading(false, 0.5f);
        yield return new WaitForSeconds(0.5f);
        fader.FadeOut(1f);
        timer.StartTimer();
    }

    public void EndGame(bool won) {
        if (state != GameState.STARTED) {
            Debug.LogWarning("Trying to end a game that hasn't started!");
            return;
        }
        state = GameState.ENDED;

        timer.EndTimer();
        fader.FadeIn(2f);

        Debug.Log("You " + (won ? "won!" : "lost!"));

        //TODO: Show game over/win screen
    }

    public void Restart() {
        if (state != GameState.ENDED) {
            Debug.LogWarning("Trying to restart a game that hasn't ended!");
            return;
        }
        state = GameState.INIT;

        //TODO: Reset everything

        StartGame();
    }
}

public enum GameState {
    INIT,
    STARTED,
    ENDED
}