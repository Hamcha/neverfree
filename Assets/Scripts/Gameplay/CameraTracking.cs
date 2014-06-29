using UnityEngine;
using System.Collections.Generic;

public class CameraTracking : MonoBehaviour {
    public struct CameraObject {
        public GameObject Object;
        public float Weight;
    }

    public List<CameraObject> Objects;
    public Vector3 targetPosition;
    public SpriteRenderer terrain;

    public float posTime;

    private Vector3 posVelocity;

    CameraTracking() {
        Objects = new List<CameraObject>();
    }

    void FixedUpdate() {
        if (Objects.Count < 1) return;

        // Reset variables
        Vector3 position = Vector3.zero;
        float totalWeight = 0f;

        // Calculate position using Weighted Arithmetic Average
        foreach (CameraObject c in Objects) {
            position += c.Object.transform.position * c.Weight;
            totalWeight += c.Weight;
        }

        position /= totalWeight;

        // No terrain set? Ignore bound calculations
        if (terrain == null) return;

        // Calculate Screen size and Map size
        Vector2 dimensions = CameraTracking.CalculateScreenSizeInWorldCoords(camera);
        Bounds limits = terrain.sprite.bounds;
        Vector3 top = limits.min;
        Vector3 bot = limits.max;

        // Check if screen is bigger than map
        // if not, fit screen to map bounds
        if (dimensions.x > bot.x * 2) {
            position.x = 0;
        } else {
            if (position.x - dimensions.x / 2 < top.x) position.x = top.x + dimensions.x / 2;
            if (position.x + dimensions.x / 2 > bot.x) position.x = bot.x - dimensions.x / 2;
        }

        if (dimensions.y > bot.y * 2) {
            position.y = 0;
        } else {
            if (position.y - dimensions.y / 2 < top.y) position.y = top.y + dimensions.y / 2;
            if (position.y + dimensions.y / 2 > bot.y) position.y = bot.y - dimensions.y / 2;
        }

        targetPosition = position;
        targetPosition.z = -10f;
    }

    void Update() {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref posVelocity, posTime);
    }

    public void Add(GameObject obj, float weigth) {
        CameraObject camObj;
        camObj.Object = obj;
        camObj.Weight = weigth;
        Objects.Add(camObj);
    }

    public void Remove(GameObject obj) {
        Objects.RemoveAll(x => x.Object == obj);
    }

    public void Clear() {
        Objects.Clear();
    }

    static public Vector2 CalculateScreenSizeInWorldCoords(Camera cam) {
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float width = (p2 - p1).magnitude;
        float height = (p3 - p2).magnitude;

        Vector2 dimensions = new Vector2(width, height);

        return dimensions;
    }

    public void InstantMove() {
        FixedUpdate();
        transform.position = targetPosition;
    }
}

