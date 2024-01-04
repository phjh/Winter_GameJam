using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalKnightPattern : BossPatternBase
{
	[SerializeField] private int MaxPatternValue;
	private BossMain bossMain;
	private IEnumerator AttackCoroutine;
	private IEnumerator PassiveCoroutine;

	private int PatternNum = 0;
	private int TempPatternNum;
	private int PatternCount = 0;

	private void Awake()
	{
		bossMain = GetComponent<BossMain>();
	}


	public override void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false)
	{
		throw new System.NotImplementedException();
	}

	public override void OnDie()
	{
		throw new System.NotImplementedException();
	}

	public override void StartPattern()
	{
		UpdatePattern();
	}

	public override void UpdatePattern()
	{
		PassiveCoroutine = null;
		PassiveCoroutine = DropThunder();
	}

	#region Patterns

	private void LineThinking(int num)
	{
		switch (num)
		{
			case 1:
				KeyManager.Instance.firstline[Random.Range(0, 12)].DamageEvent(0.6f, 0.1f, true, "Thunder");
				break;
			case 2:
				KeyManager.Instance.secondline[Random.Range(0, 11)].DamageEvent(0.6f, 0.1f, true, "Thunder");
				break;
			case 3:
				KeyManager.Instance.thirdline[Random.Range(0, 10)].DamageEvent(0.6f, 0.1f, true, "Thunder");
				break;
			case 4:
				KeyManager.Instance.fourthline[Random.Range(0, 9)].DamageEvent(0.6f, 0.1f, true, "Thunder");
				break;
		}
	}

	private IEnumerator DropThunder()
	{
		int dropPos;
		for(int i = 0; i < 3; i++)
		{
			dropPos = Random.Range(1, 5);
			LineThinking(dropPos);
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(4f);
		UpdatePattern();
	}

	private IEnumerator LeftThunder(float time, float duration, float waitTime = 0.5f)
	{
		yield return new WaitForSeconds(2f);
		KeyManager.Instance.MainBoard[(int)RowKey.Y].DamageEvent(0.5f, 2f, true, "Thunder");
		for (int i = 5; i >= 0; i--)
		{
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder");
			if(i - 1 >= 0) KeyManager.Instance.secondline[i - 1].DamageEvent(time, duration, true, "Thunder");
			if(i - 1 >= 0) KeyManager.Instance.thirdline[i - 1].DamageEvent(time, duration, true, "Thunder");
			if(i - 2 >= 0) KeyManager.Instance.fourthline[i - 2].DamageEvent(time, duration, true, "Thunder");
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	#endregion

}
