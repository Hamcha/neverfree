using UnityEngine;
using System.Collections;

public class CinematicShotEffect : MonoBehaviour {

	public enum ShotEffectType {
		OddOffset
	}

	public ShotEffectType effect;
	public float strength = 1;
	public float speed = 1; // might be "duration" under certain effects
	public float delay = 7;
	public float duration = 0; // 0 = endless

	float startTime, transitionTime;
	bool active = false;
	GameObject[] shots;

	void Start() {
		StartCoroutine(WaitDelay());
	}

	void Update () {
		if (!active) return;
		if (duration > 0 && Time.time - startTime > duration) {
			Debug.Log("Finished shot effect (" + effect.ToString() + ")");
			active = false;
		}
		switch (effect) {
			case ShotEffectType.OddOffset:
				if (startTime - Time.time < speed) {
					for (int i = 0; i < shots.Length; i++) {
						float parity = (i % 2) * 2 - 1;
						shots[i].transform.position = Vector3.Lerp(Vector3.zero,
																   new Vector3(parity * strength + Mathf.Sin(i), 
																	           parity * strength + Mathf.Cos(i), 
																			   0), 
																   (Time.time - startTime) / speed);
					}
				}
				break;
		}
	}

	IEnumerator WaitDelay() {
		yield return new WaitForSeconds(delay);
		Debug.Log("Starting shot effect (" + effect.ToString() + ")");
		shots = GetComponent<CinematicShotFactory>().shots.ToArray();
		transitionTime = startTime = Time.time;
		active = true;
	}
}
