using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    public Image black;

    public bool showLoaderByDefault = true;
    public GameObject loader;

    public Color fadeColor;

    void Awake() {
        if (showLoaderByDefault) {
            ShowLoading(true, 0);
        } else {
            ShowLoading(false, 0);
        }
    }

    public void FadeIn(float duration) {
        black.CrossFadeColor(fadeColor, duration, false, true);
    }

    public void FadeOut(float duration) {
        Color fadeOutColor = fadeColor;
        fadeOutColor.a = 0;
        black.CrossFadeColor(fadeOutColor, duration, false, true);
    }

    public void ShowLoading(bool show, float fadeDuration) {
        Image image = loader.GetComponent<Image>();
        if (image != null) {
            image.CrossFadeAlpha(show ? 1 : 0, fadeDuration, true);
        }

        Text text = loader.GetComponent<Text>();
        if (text != null) {
            text.CrossFadeAlpha(show ? 1 : 0, fadeDuration, true);
        }
    }

}