using UnityEngine;
using UnityEngine.UI;

public class SaveEditor : MonoBehaviour {

    public GameObject[] stances;
    private string currentStance;

    public void Awake() {
        for (int i = 0; i < stances.Length; i++) {
            stances[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }

        //todo basic loading of save
        //todo fill name/hearts/coins
        //todo highlight enabled stances
    }

    public void setCurrentStance(string stanceName) {
        currentStance = stanceName;
    }

    public void changedStanceStatus(bool enabled) {
        SetFocus(currentStance, enabled);
        //todo generate list and put it somewhere
    }

    private void SetFocus(string name, bool dofocus) {
        for (int i = 0; i < stances.Length; i++) {
            if (stances[i].name == name) {
                stances[i].GetComponent<Image>().color = new Color(1, 1, 1, dofocus ? 1 : 0.3f);
                return;
            }
        }
    }
}
