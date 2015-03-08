using UnityEngine;
using System.Collections;

public class SaveTrigger : MonoBehaviour {
    public Animator[] beams;
    public SpriteRenderer highlight;
    public ScrollText scroller;
    private bool playerInTrigger = false;
    private bool saving = false;
    private bool saved = false;
    private float lightVelocity = 0f;

    void Update() {
        Color c = highlight.color;
        // We don't need this is the player is outside the platform
        if (!playerInTrigger || saved) {
            c.a = Mathf.SmoothDamp(c.a, 0, ref lightVelocity, 0.5f);
            highlight.color = c;
            return;
        }

        // Increase to half tone when on the platform
        c.a = Mathf.SmoothDamp(c.a, 1, ref lightVelocity, 0.5f);
        highlight.color = c;

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
            Player.Instance.Save();
            // We're done!
            foreach (Animator beam in beams) {
                beam.SetTrigger("Done");
            }
            scroller.setText("Save complete!", 0.05f);
            PlayerScript.instance.disabled = false;
            saving = false;
            saved = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player" && !playerInTrigger) {
            playerInTrigger = true;
            scroller.setText("Press E to save..", 0.05f);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Player" && playerInTrigger) {
            playerInTrigger = false;
            scroller.delText(0.03f);
        }
    }
}
