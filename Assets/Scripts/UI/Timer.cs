using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    public int seconds = 300;
    private float timeLeft = 0;
    private bool running = false;

    public Text text;
    public Color minuteColor = Color.red;
    private Color originalColor;

    public UnityEvent onTimerEnd;

    void Awake() {
        originalColor = text.color;
        timeLeft = seconds;
    }

    public void StartTimer() {
        text.color = originalColor;
        timeLeft = seconds;
        running = true;
    }

    public void EndTimer() {
        running = false;
    }

    void Update() {
        //Don't do anything when timer isn't running
        if (!running) {
            return;
        }

        //Timer has ran out of time
        if (timeLeft <= 0) {
            onTimerEnd.Invoke();
            running = false;
            return;
        }

        //Change color last minute
        if (timeLeft <= 60 && text.color == originalColor) {
            text.color = minuteColor;
        }

        //Set time
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = (int)timeLeft - minutes * 60;
        text.text = (minutes < 10 ? "0" + minutes : minutes.ToString()) + ":" + (seconds < 10 ? "0" + seconds : seconds.ToString());

        //Decrease time
        timeLeft -= Time.deltaTime;
    }
    
}