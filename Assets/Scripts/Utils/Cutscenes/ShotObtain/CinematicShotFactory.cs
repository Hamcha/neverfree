using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicShotFactory : MonoBehaviour {
	public GameObject template = null;
	public int count = 5;
	public float delay = 0;
	public float interval = 0.5f;
	public List<GameObject> shots;

	void Start() {
		shots = new List<GameObject>();
		StartCoroutine(SpawnObjects());
	}

	IEnumerator SpawnObjects() {
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < count; i++) {
			shots.Add((GameObject)Instantiate(template, transform.position, transform.rotation));
			yield return new WaitForSeconds(interval);
		}
	}
}
