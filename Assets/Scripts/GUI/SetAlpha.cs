using UnityEngine;
using System.Collections;

public class SetAlpha : MonoBehaviour 
{
    public float alpha;
    public bool destroyOnFadeOut = false;

	void Update () 
    {
        Color newc = guiText.color;
        newc.a = alpha;
        guiText.color = newc;
        if (destroyOnFadeOut && alpha <= 0) Destroy(gameObject);
	}
}
