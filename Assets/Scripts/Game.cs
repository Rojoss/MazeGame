using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Game : MonoBehaviour {

    private GameState state = GameState.INIT;

    public Fader fader;
    public Timer timer;

    public Sprite gameOverScreen;
    public Sprite winScreen;

    public static Game Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        StartGame();
    }

    void OnDestroy() {
        Instance = null;
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
        fader.GetComponent<Image>().sprite = (won ? winScreen : gameOverScreen);
        fader.fadeColor = Color.white;
        fader.black.color = Color.white;
        fader.FadeIn(2f);

        FindObjectOfType<FirstPersonController>().inputEnabled = false;
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