using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RiddlePopup : Popup {

    public Text riddleText;
    public InputField answerInput;

    private Riddle riddle;

    public static RiddlePopup Instance { get; private set; }

    public void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        gameObject.SetActive(false);
    }

    public void setRiddle(Riddle riddle) {
        this.riddle = riddle;
        riddleText.text = riddle.riddle;
    }

    public string getInput() {
        return answerInput.text;
    }

    public void onUpdateInput() {
        
    }

    public void onSubmitInput() {
        if (riddle != null) {
            riddle.onSubmit(getInput());
        }
    }

    public void reset() {
        riddleText.text = "???";
        answerInput.text = "";
    }

    public void resetInput() {
        answerInput.text = "";
    }

    public override void onOpen() {
        answerInput.Select();
        answerInput.ActivateInputField();
    }

    public override void onClose() {
        answerInput.DeactivateInputField();
        reset();
    }
}