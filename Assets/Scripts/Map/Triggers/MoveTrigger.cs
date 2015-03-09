using UnityEngine;

public class MoveTrigger : MonoBehaviour {

    public string targetLevel;
    public string targetBorder;
    public bool side, vertical;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Player.Instance.transitionBorder = targetBorder;
            if (targetLevel == "_LAST_")
                targetLevel = Player.Instance.data.map;
            Player.Instance.data.map = Application.loadedLevelName;
            GUIScript.instance.transition.Goto(targetLevel, side, vertical);
        }
    }
}
