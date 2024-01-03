using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorChanger : MonoSingleton<LightColorChanger>
{
    private Light2D MoonLight;
	private IEnumerator LerpingCoroutine = null;
	[SerializeField] private Color[] Colors;

	private void Awake()
	{
		MoonLight = GetComponent<Light2D>();
	}

	public void ChangeColor(int num)
	{
		if(LerpingCoroutine != null)
		{
			StopAllCoroutines();
			LerpingCoroutine = null;
		}
		LerpingCoroutine = ColorLerping(num);
		StartCoroutine(LerpingCoroutine);
	}

	private IEnumerator ColorLerping(int num)
	{
		float t = 0;
		Color defaultColor = MoonLight.color;
		while(t < 1)
		{
			MoonLight.color = new Color(Mathf.Lerp(defaultColor.r, Colors[num].r, t), Mathf.Lerp(defaultColor.g, Colors[num].g, t), Mathf.Lerp(defaultColor.b, Colors[num].b, t));
			t += Time.deltaTime;
			yield return null;
		}
		LerpingCoroutine = null;
	}
}
