using UnityEngine;
using System.Collections;

public class RemoveInterpolation : MonoBehaviour {
	void Start () {
        renderer.material.mainTexture.filterMode = FilterMode.Point;
	}
}
