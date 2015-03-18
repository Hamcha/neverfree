using UnityEngine;
using System.Collections;

public class CinematicShotFactory : MonoBehaviour {
	public GameObject template = null;
    public int count = 5;
    public float delay = 0;
    public float interval = 0.5f;

	void Start () {
		StartCoroutine(SpawnObjects());
	}

	IEnumerator SpawnObjects() {
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < count; i++) {
			Instantiate(template, transform.position, transform.rotation);
			yield return new WaitForSeconds(interval);
		}
	}
}
