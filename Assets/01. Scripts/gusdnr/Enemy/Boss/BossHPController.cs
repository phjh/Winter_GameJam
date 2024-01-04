using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPController : MonoBehaviour
{
	[Header("HP Gage")]
	public Image FillingImage;
	private BossMain bossMain;

	private void Start()
	{
		bossMain = GetComponent<BossMain>();
	}

	private void Update()
	{
		
	}

	public void SetFillAmount()
	{
		FillingImage.fillAmount = bossMain.CurHP / bossMain.MaxHP;
	}
}
