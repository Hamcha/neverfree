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
            GetComponent<GUIText>().text = text.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator StartDelete(float delay) {
        while (GetComponent<GUIText>().text.Length > 1) {
            GetComponent<GUIText>().text = GetComponent<GUIText>().text.Substring(0, GetComponent<GUIText>().text.Length - 2);
            if (GetComponent<GUIText>().text.Length > 0 && GetComponent<GUIText>().text[GetComponent<GUIText>().text.Length - 1] == ' ') continue;
            yield return new WaitForSeconds(delay);
        }
        GetComponent<GUIText>().text = "";
    }
}
