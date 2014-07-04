using UnityEngine;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {
    public GameObject Heart;
    public Camera UICamera;

    private int health;
    private List<GameObject> hearts;

    HealthBar() {
        hearts = new List<GameObject>();
    }

    void Start() {
        health = Player.Instance.health;
        for (int i = 0; i < health / 2; i++) {
            GameObject heart = (GameObject)Instantiate(Heart);
            heart.transform.parent = transform;
            DockUI dock = heart.GetComponent<DockUI>();
            // Divide into rows
            float x = i % 10;
            float y = Mathf.Floor(i / 10);
            // Hex-style positioning
            float yoff = x % 2;
            dock.offset = new Vector2(3 + 5 * x, -8 - 4 * yoff - 8 * y);
            hearts.Add(heart);
        }

        Player.Instance.HealthChanged += (instance, newHealth) => {
            health = newHealth;
            for (int i = 0; i < hearts.Count; i++) {
                int status = health / 2 > i ? 0 :
                    health / 2 == i && health % 2 != 0 ? 1 : 2;
                hearts[i].GetComponent<Animator>().SetInteger("status", status);
            }
        };
    }
}
