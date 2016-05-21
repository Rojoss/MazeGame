using UnityEngine;
using System.Collections;

public class Riddle : MonoBehaviour {

    public string riddle;
    public string tip;
    public string[] answers;
    public string correctInstruction;
    public string[] wrongInstructions;

    [HideInInspector]
    public string answer;
    [HideInInspector]
    public string instruction;

    void OnMouseDown() {
        RiddlePopup.Instance.setRiddle(this);
        RiddlePopup.Instance.Open();
    }

    public void onSubmit(string answer) {
        this.answer = answer;
        answer = answer.ToLower().Trim();
        if (answer.Length == 0) {
            return;
        }
        for (int i = 0; i < answers.Length; i++) {
            if (answers[i].Equals(answer)) {
                onAnswer(true);
                return;
            }
        }
        onAnswer(false);
    }

    private void onAnswer(bool correct) {
        if (correct) {
            instruction = correctInstruction;
        } else {
            instruction = wrongInstructions[Random.Range(0, wrongInstructions.Length)];
        }
        RiddlePopup.Instance.setRiddle(this);
    }
}