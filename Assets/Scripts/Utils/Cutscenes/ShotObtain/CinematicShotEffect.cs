using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicShotEffect : MonoBehaviour {
	public enum ShotEffectType {
		Rotation,
		OddOffset,
		ChangeSpeed
	}

	public ShotEffectType effect;
	public float strength = 1;
	public float transitionTime = 1;
	public float delay = 7;
	public float duration = 0; // 0 = endless
	public bool reset = false;

	float startTime;
	bool active = false, ready = false, fadeout = false;
	GameObject[] shots;

	// Effect-specific variables
	Vector3[] targetPos;
	Animator[] animators;

	void Start() {
		StartCoroutine(WaitDelay());
	}

	void Update() {
		// Don't bother until we have shots to process
		if (!ready) return;

		// The first effect should reset the position
		if (reset) {
			for (int i = 0; i < shots.Length; i++) {
				shots[i].transform.position = Vector3.zero;
			}
		}

		// If the effect is finished skip everything else
		if (!active) return;

		// Is this effect over?
		if (duration > 0 && Time.time - startTime > duration) {
			// If the fadeout finished, deactivate the effect
			if (Time.time - startTime > duration + transitionTime) {
				Debug.Log("Finished shot effect (" + effect.ToString() + ")");
				active = false;
			}
				// Otherwise, start the fadeout
			else {
				fadeout = true;
			}
		}

		for (int i = 0; i < shots.Length; i++) {
			float t = Mathf.Min(1f, (Time.time - startTime) / transitionTime);

			// Reverse on fadeout
			if (fadeout) {
				t = Mathf.Max(0, 1 - (Time.time - startTime - duration) / transitionTime);
			}

			ApplyEffect(i, t);
		}
	}

	IEnumerator WaitDelay() {
		yield return new WaitForSeconds(delay);
		Debug.Log("Starting shot effect (" + effect.ToString() + ")");
		shots = GetComponent<CinematicShotFactory>().shots.ToArray();
		PrepareEffect();
		startTime = Time.time;
		active = ready = true;
	}

	// Prepare for effect (eg. Fill up target positions)
	void PrepareEffect() {
		switch (effect) {
			case ShotEffectType.Rotation:
				targetPos = new Vector3[shots.Length];
				for (int i = 0; i < shots.Length; i++) {
					float tot = ((float)i) / shots.Length;
					targetPos[i] = new Vector3(Mathf.Sin(tot * 2 * Mathf.PI) * strength, Mathf.Cos(tot * 2 * Mathf.PI) * strength, 0);
				}
				break;
			case ShotEffectType.OddOffset:
				targetPos = new Vector3[shots.Length];
				for (int i = 0; i < shots.Length; i++) {
					targetPos[i] = i % 2 != 0 ? new Vector3(Mathf.Sin(i) * strength, Mathf.Cos(i) * strength, 0) : Vector3.zero;
				}
				break;
			case ShotEffectType.ChangeSpeed:
				animators = new Animator[shots.Length];
				for (int i = 0; i < shots.Length; i++) {
					animators[i] = shots[i].GetComponentInChildren<Animator>();
				}
				break;
		}
	}

	void ApplyEffect(int i, float t) {
		switch (effect) {
			case ShotEffectType.Rotation:
			case ShotEffectType.OddOffset:
				shots[i].transform.position += Vector3.Lerp(Vector3.zero, targetPos[i], t);
				break;
			case ShotEffectType.ChangeSpeed:
				animators[i].speed = Mathf.Lerp(1, strength, t);
				break;
		}
	}
}
