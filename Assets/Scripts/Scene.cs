using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour 
{
    public GameObject player;
    public static CameraTracking tracker;
    public HealthBar healthBar;
    public StanceBar stanceBar;

    void Start()
    {
        // Setup tracking camera
        Scene.tracker = Camera.main.GetComponent<CameraTracking>();
        Scene.tracker.Add(player, 100f);
    }
}
