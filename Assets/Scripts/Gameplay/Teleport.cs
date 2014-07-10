using UnityEngine;

public class Teleport : MonoBehaviour {
    public RangeSprite range;
    public bool teleporting = false;
    public GameObject blinkPrefab, phantomPrefab;
    public bool isPlayerClose { 
        get { return Vector2.Distance(Cursor.instance.transform.position, transform.position) < range.currentScale.x / 2;} 
    }

    void Awake() {
        range = GetComponent<RangeSprite>();
    }

    void FixedUpdate() {
        Cursor.instance.Teleport(isPlayerClose);
    }

    public void TeleportPlayer(GameObject player) {
        if (!isPlayerClose) return;
        if (BlockTeleport.blocked) return;
        Vector3 targetPosition = Cursor.instance.transform.position;
        Vector3 oldPosition = player.transform.position;
        targetPosition.z = oldPosition.z;
        player.transform.position = targetPosition;
        Instantiate(blinkPrefab, player.transform.position, Quaternion.identity);
        Instantiate(phantomPrefab, oldPosition, Quaternion.identity);
    }
}
