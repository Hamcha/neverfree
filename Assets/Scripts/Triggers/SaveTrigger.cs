using UnityEngine;
using System.Collections;

public class SaveTrigger : MonoBehaviour {
    public Animator[] beams;
    public Light light;
    private bool playerInTrigger = false;
    private bool saving = false;
    private bool saved = false;
    private float lightVelocity = 0f;

    void Update() {
        // We don't need this is the player is outside the platform
        if (!playerInTrigger || saved) {
            light.intensity = Mathf.SmoothDamp(light.intensity, 0, ref lightVelocity, 0.5f);
            return;
        }

        // Increase to half tone when on the platform
        light.intensity = Mathf.SmoothDamp(light.intensity, 0.7f, ref lightVelocity, 0.5f);

        // Save when player in trigger AND pressed E
        if (Input.GetKey(KeyCode.E) && !saving) {
            saving = true;
            PlayerScript.instance.disabled = true;
            foreach (Animator beam in beams) {
                beam.SetTrigger("Start");
            }
            // Refill player health
            Player.Instance.health = Player.Instance.data.hearts * 2;
            // Save player data
            Player.Instance.data.Save();
            // We're done!
            foreach (Animator beam in beams) {
                beam.SetTrigger("Done");
            }
            PlayerScript.instance.disabled = false;
            saving = false;
            saved = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Player") {
            playerInTrigger = false;
        }
    }
}
