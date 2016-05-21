using UnityEngine;
using System.Collections.Generic;

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

    private Dictionary<string, int> previousAnswers = new Dictionary<string, int>();

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
                onAnswer(answer, true);
                return;
            }
        }
        onAnswer(answer, false);
    }

    private void onAnswer(string answer, bool correct) {
        if (previousAnswers.ContainsKey(answer)) {
            instruction = wrongInstructions[previousAnswers[answer]];
        } else {
            if (correct) {
                instruction = correctInstruction;
            } else {
                int r = Random.Range(0, wrongInstructions.Length);
                previousAnswers.Add(answer, r);
                instruction = wrongInstructions[r];
            }
        }
        
        RiddlePopup.Instance.setRiddle(this);
    }
}