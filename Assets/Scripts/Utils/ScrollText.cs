using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour {
    public void setText(string text, float delay) {
        StartCoroutine(StartScrolling(text, delay));
    }

    public void delText(float delay) {
        StartCoroutine(StartDelete(delay));
    }

    IEnumerator StartScrolling(string text, float delay) {
        for (int i = 0; i <= text.Length; i++) {
            if (i < text.Length && text[i] == ' ') continue;
            guiText.text = text.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator StartDelete(float delay) {
        while (guiText.text.Length > 1) {
            guiText.text = guiText.text.Substring(0, guiText.text.Length - 2);
            if (guiText.text.Length > 0 && guiText.text[guiText.text.Length - 1] == ' ') continue;
            yield return new WaitForSeconds(delay);
        }
        guiText.text = "";
    }
}
