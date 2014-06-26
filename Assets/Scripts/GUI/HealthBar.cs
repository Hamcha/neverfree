using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public GameObject Heart;
    public Camera UICamera;

    private int heartCount;

    void Start() {
        heartCount = Player.Instance.health;
        for (int i = 0; i < heartCount; i++) {
            GameObject heart = (GameObject)Instantiate(Heart);
            heart.transform.parent = transform;
            DockUI dock = heart.GetComponent<DockUI>();
            dock.UICamera = UICamera;
            dock.offset.x = 2 + 7 * i;
        }

        Player.Instance.HealthChanged += (instance, newHealth) => {
            heartCount = newHealth;
        };
    }
}
