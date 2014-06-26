using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {
    public GameObject Heart;
    public Camera UICamera;

    private int heartCount;
    private List<GameObject> hearts;

    HealthBar() {
        hearts = new List<GameObject>();
    }

    void Start() {
        heartCount = Player.Instance.health;
        for (int i = 0; i < heartCount / 2; i++) {
            GameObject heart = (GameObject)Instantiate(Heart);
            heart.transform.parent = transform;
            DockUI dock = heart.GetComponent<DockUI>();
            dock.offset.x = 2 + 7 * i;
            hearts.Add(heart);
        }

        Player.Instance.HealthChanged += (instance, newHealth) => {
            heartCount = newHealth;
            for (int i = 0; i < hearts.Count; i++) {
                int status = heartCount / 2 > i ? 0 :
                    heartCount / 2 == i && heartCount % 2 != 0 ? 1 : 2;
                hearts[i].GetComponent<Animator>().SetInteger("status", status);
            }
        };
    }
}
