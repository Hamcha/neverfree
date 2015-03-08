using UnityEngine;
using UnityEngine.UI;

public class SaveEditor : MonoBehaviour {

    public GameObject[] abilities;
    private string currentAbility;

    public void Awake() {
        for (int i = 0; i < abilities.Length; i++) {
            abilities[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }

        //todo basic loading of save
        //todo fill name/hearts/coins
        //todo highlight enabled abilities
    }

    public void setCurrentAbility(string abilityName) {
        currentAbility = abilityName;
    }

    public void changedAbilityStatus(bool enabled) {
        SetFocus(currentAbility, enabled);
        //todo generate list and put it somewhere
    }

    private void SetFocus(string name, bool dofocus) {
        for (int i = 0; i < abilities.Length; i++) {
            if (abilities[i].name == name) {
                abilities[i].GetComponent<Image>().color = new Color(1, 1, 1, dofocus ? 1 : 0.3f);
                return;
            }
        }
    }
}
