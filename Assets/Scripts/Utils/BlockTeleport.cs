using System.Collections.Generic;
using UnityEngine;

public class BlockTeleport : MonoBehaviour {

    public static bool blocked { 
        get {
            foreach (BlockTeleport t in blockingObjects)
                if (t.thisBlocked) return true;
            return false;
        } 
    }
    private static HashSet<BlockTeleport> blockingObjects;
    public bool thisBlocked;

    void Awake() {
        if (blockingObjects == null)
            blockingObjects = new HashSet<BlockTeleport>();
        blockingObjects.Add(this);
    }

    void OnMouseEnter() {
        thisBlocked = true;
    }

    void OnMouseExit() {
        thisBlocked = false;
    }

    void OnDestroy() {
        blockingObjects.Remove(this);
    }
}
