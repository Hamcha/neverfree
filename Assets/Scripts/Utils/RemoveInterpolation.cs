using UnityEngine;
using System.Collections;

public class RemoveInterpolation : MonoBehaviour {
	void Start () {
        GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Point;
	}
}
