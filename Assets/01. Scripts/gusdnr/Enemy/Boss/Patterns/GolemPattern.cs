using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemPattern : BossPatternBase
{
	[SerializeField] private int MaxPatternValue;
	private BossMain bossMain;
	private IEnumerator AttackCoroutine;
	
	private int PatternNum = 0;
	private int TempPatternNum;
	private int PatternCount = 0;

	private void Awake()
	{
		bossMain = GetComponent<BossMain>();
	}

	public override void StartPattern()
	{
		bossMain.SetAnimation("idle");
		if(AttackCoroutine != null)
		{
			Debug.LogError("패턴 코루틴이 초기화되지 않았습니다.");
			StopAllCoroutines();
			ChangePattern();
		}
		ChangePattern();
	}

	public override void OnDie()
	{
		StopAllCoroutines();
		AttackCoroutine = null;
		gameObject.SetActive(false);
		BossPatternBase bossPattern = this;
		bossPattern.enabled = false;
	}

	public override void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false)
	{
		AttackCoroutine = null;
		if (PatternCount == 5)
		{
			AttackCoroutine = CratorBurst();
			bossMain.SetAnimation("skill_2");
			StartCoroutine(AttackCoroutine);
			PatternCount = 0;
			return;
		}
		if (isFixedLink)
			PatternNum = LinkedPattern;
		else
		{
			PatternNum = Random.Range(0, MaxPatternValue);
			if (TempPatternNum == PatternNum)
			{
				ChangePattern();
				return;
			}
		}
		TempPatternNum = PatternNum;
		LightColorChanger.Instance.ChangeColor(PatternNum);
		PatternCount++;
		switch (PatternNum)
		{
			case 0:
				AttackCoroutine = WaveGround(0.3f, 0.1f);
				break;
			case 1:
				AttackCoroutine = SmashGround(0.4f, 0.6f, 1.2f);
				break;
			case 2:
				AttackCoroutine = BoomGround(0.7f, 0.3f, 1.5f);
				break;
			case 3:
				AttackCoroutine = DiagonalBoom(0.6f, 0.2f, 1f);
				break;
			case 4:
				AttackCoroutine = CrossBoom(0.4f, 0.2f);
				break;
			default:
				break;
		}
		bossMain.SetAnimation("skill_1");
		StartCoroutine(AttackCoroutine);
	}

	public override void UpdatePattern()
	{
		//골렘은 사용 X
	}

	#region Patterns

	private IEnumerator WaveGround(float time, float duration, float waitTime = 0.5f)
	{
		for (var i = 0; i < 12; i++)
		{
			if (i > KeyManager.Instance.firstline.Count) break;
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "GroundBoom");
			if (i < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "GroundBoom");
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		for (var i = 0; i < 10; i++)
		{
			if (i > KeyManager.Instance.thirdline.Count) break;
			if (i < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "GroundBoom");
			if (i < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "GroundBoom");
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern(3, true);
	}

	private void NearAreaAttack(float time, float duration, KeyBase Center, bool centerAttack = false)
	{
		if (centerAttack) Center.DamageEvent(time, duration, true, "GroundBigExplosion");

		foreach (KeyBase area in Center.connectedKeys)
		{
			area.DamageEvent(time, duration, true, "GroundBoom");
		}
	}

	private IEnumerator SmashGround(float time, float duration, float waitTime = 0.5f)
	{
		int center = Random.Range(0, (int)RowKey.Period + 1);
		for (int i = 0; i < 3; i++)
		{
			center = Random.Range(0, (int)RowKey.Period + 1);
			NearAreaAttack(time, duration, KeyManager.Instance.MainBoard[center]);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator BoomGround(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i < 12; i++)
		{
			if (i > KeyManager.Instance.firstline.Count) break;
			if (i % 2 == 1) continue;
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "GroundBoom");
			if (i < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "GroundBoom");
			if (i < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "GroundBoom");
			if (i < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "GroundBoom");
			yield return new WaitForSeconds(time);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator DiagonalBoom(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i <= 9; i++)
		{
			if (i % 2 == 0)
			{
				if (i < KeyManager.Instance.firstline.Count) KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i + 1 < KeyManager.Instance.firstline.Count) KeyManager.Instance.firstline[i + 1].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i + 1 < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[i + 1].DamageEvent(time, duration, true, "GroundBigExplosion");
			}
			else if (i % 2 == 1)
			{
				if (i < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i + 1 < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[i + 1].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "GroundBigExplosion");
				if (i + 1 < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[i + 1].DamageEvent(time, duration, true, "GroundBigExplosion");
			}
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator CrossBoom(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i < 12; i++)
		{
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "GroundBoom");
			KeyManager.Instance.secondline[11 - i].DamageEvent(time, duration, true, "GroundBoom");
			KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "GroundBoom");
			KeyManager.Instance.fourthline[11 - i].DamageEvent(time, duration, true, "GroundBoom");
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator CratorBurst()
	{
		NearAreaAttack(0.3f, 2f, KeyManager.Instance.MainBoard[(int)RowKey.Y], true);
		for (int i = 0; i < KeyManager.Instance.fourthline.Count; i++)
		{
			KeyManager.Instance.fourthline[i].DamageEvent(0.7f, 2.5f, true, "GroundBigExplosion");
		}
		for (int i = 0; i < 3; i++)
		{
			KeyManager.Instance.firstline[5 - i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			KeyManager.Instance.secondline[4 - i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			KeyManager.Instance.thirdline[3 - i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			KeyManager.Instance.firstline[6 + i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			KeyManager.Instance.thirdline[6 + i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			KeyManager.Instance.secondline[6 + i].DamageEvent(1f, 1.6f, true, "GroundBoom");
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(3f);
		ChangePattern();
	}
	#endregion
}
