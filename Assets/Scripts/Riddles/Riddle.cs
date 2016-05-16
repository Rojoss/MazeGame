using UnityEngine;
using System.Collections;

public class Riddle : MonoBehaviour {

    public string riddle;
    public string[] answers;

    void OnMouseDown() {
        RiddlePopup.Instance.setRiddle(this);
        RiddlePopup.Instance.Open();
    }

    public void onSubmit(string answer) {
        RiddlePopup.Instance.Close();
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
        Debug.Log("Answer is " + (correct ? "correct" : "incorrect") + "!");
        //TODO: Show direction
    }
}