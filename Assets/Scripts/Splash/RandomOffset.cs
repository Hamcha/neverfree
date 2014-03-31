using UnityEngine;
using System.Collections;

public class RandomOffset : MonoBehaviour 
{
    public float magnitude;
    public float startDelay;
    public float repeatDelay;

    void Start()
    {
        InvokeRepeating("RandomUV", startDelay, repeatDelay);
    }

	void RandomUV() 
    {
        float x = Random.value * magnitude - magnitude / 2;
        float y = Random.value * magnitude - magnitude / 2;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(x, y));
	}
}
