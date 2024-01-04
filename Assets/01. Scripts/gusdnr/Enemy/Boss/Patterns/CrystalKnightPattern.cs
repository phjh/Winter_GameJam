using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

	public override void StartPattern()
	{
		UpdatePattern();
		ChangePattern(2, true);
	}

	public override void UpdatePattern()
	{
		PassiveCoroutine = null;
		PassiveCoroutine = DropThunder();
		StartCoroutine(PassiveCoroutine);
	}

	public override void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false)
	{
		AttackCoroutine = null;
		if (PatternCount == 5)
		{
			AttackCoroutine = ThunderBurst(0.5f, 0.2f, 2f);
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
		PatternCount += 1;
		switch (PatternNum)
		{
			case 0:
				AttackCoroutine = LeftThunder(0.5f, 0.2f);
				break;
			case 1:
				AttackCoroutine = RightThunder(0.5f, 0.2f);
				break;
			case 2:
				AttackCoroutine = MiddleThunder(0.7f, 0.4f);
				break;
			case 3:
				AttackCoroutine = SquareThunder(0.8f, 0.3f);
				break;
			case 4:
				AttackCoroutine = ThunderWave(0.4f, 0.1f, 1.5f);
				break;
			case 5:
				AttackCoroutine = SliceThunder(0.9f, 0.3f, Random.Range(2, 6), 2f);
				break;
			default:
				Debug.LogError("보스의 어택 코루틴이 할당되지 않았습니다!");
				break;
		}
		StartCoroutine(AttackCoroutine);
	}

	public override void OnDie()
	{
		StopAllCoroutines();
		AttackCoroutine = null;
		PassiveCoroutine = null;
		bossMain.SetAnimation("Die");
		BossPatternBase bossPattern = this;
		bossPattern.enabled = false;
	}

	#region Patterns

	private void LineThinking(int num)
	{
		switch (num)
		{
			case 1:
				KeyManager.Instance.firstline[Random.Range(0, 12)].DamageEvent(0.6f, 0.1f, true, "PassiveThunder");
				break;
			case 2:
				KeyManager.Instance.secondline[Random.Range(0, 11)].DamageEvent(0.6f, 0.1f, true, "PassiveThunder");
				break;
			case 3:
				KeyManager.Instance.thirdline[Random.Range(0, 10)].DamageEvent(0.6f, 0.1f, true, "PassiveThunder");
				break;
			case 4:
				KeyManager.Instance.fourthline[Random.Range(0, 9)].DamageEvent(0.6f, 0.1f, true, "PassiveThunder");
				break;
		}
	}

	private IEnumerator DropThunder()
	{
		int dropPos;
		for(int i = 0; i < 3; i++)
		{
			dropPos = Random.Range(1, 5);
			LineAlarmController.Instance.ActiveAlarm(dropPos);
			yield return new WaitForSeconds(1f);
			LineThinking(dropPos);
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(4f);
		UpdatePattern();
	}

	private IEnumerator LeftThunder(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackRight");
		KeyManager.Instance.MainBoard[(int)RowKey.Y].DamageEvent(0.5f, 2f, true, "Thunder3");
		KeyManager.Instance.MainBoard[(int)RowKey.B].DamageEvent(0.5f, 2f, true, "Thunder3");
		int safeArea = Random.Range(0, 6);
		for (int i = 5; i >= 0; i--)
		{
			if(i == safeArea)
			{
				yield return new WaitForSeconds(0.2f);
				continue;
			}
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder3");
			if(i - 1 >= 0) KeyManager.Instance.secondline[i - 1].DamageEvent(time, duration, true, "Thunder3");
			if(i - 1 >= 0) KeyManager.Instance.thirdline[i - 1].DamageEvent(time, duration, true, "Thunder3");
			if(i - 2 >= 0) KeyManager.Instance.fourthline[i - 2].DamageEvent(time, duration, true, "Thunder3");
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator RightThunder(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackLeft");
		KeyManager.Instance.MainBoard[(int)RowKey.Y].DamageEvent(0.5f, 2f, true, "Thunder2");
		KeyManager.Instance.MainBoard[(int)RowKey.B].DamageEvent(0.5f, 2f, true, "Thunder2");
		int safeArea = Random.Range(6, 12);
		for (int i = 6; i < 12; i++)
		{
			if (i == safeArea)
			{
				yield return new WaitForSeconds(0.2f);
				continue;
			}
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.secondline[i - 1].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.thirdline[i - 1].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.fourthline[i - 1].DamageEvent(time, duration, true, "Thunder2");
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator MiddleThunder(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackMiddle");
		for (int i = 4; i <= 7; i++) KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder1");
		yield return new WaitForSeconds(0.3f);
		for (int i = 3; i <= 7; i++) KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "Thunder2");
		yield return new WaitForSeconds(0.3f);
		for (int i = 2; i <= 7; i++) KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "Thunder3");
		yield return new WaitForSeconds(0.3f);
		KeyManager.Instance.fourthline.ForEach(keyBase =>
		{
			keyBase.DamageEvent(0.7f, 0.2f, true, "ThunderPulse");
		});
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator SquareThunder(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackWave");
		for (int i = 0; i < 12; i++)
		{
			if(i % 2 == 0)
			{
				KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder1");
				if(i + 1 < 12) KeyManager.Instance.firstline[i + 1].DamageEvent(time, duration, true, "Thunder1");
				KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "Thunder1");
				if(i + 1 < 12) KeyManager.Instance.secondline[i + 1].DamageEvent(time, duration, true, "Thunder1");
			}
			else
			{
				KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "Thunder1");
				if (i + 1 < 12) KeyManager.Instance.thirdline[i + 1].DamageEvent(time, duration, true, "Thunder1");
				KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "Thunder1");
				if (i + 1 < 12) KeyManager.Instance.fourthline[i + 1].DamageEvent(time, duration, true, "Thunder1");
			}
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator ThunderWave(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackWave");
		for (int i = 0; i <= 6; i++)
		{
			if(i % 2 == 0)
			{
				if(i * 2 < 12) KeyManager.Instance.firstline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.firstline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.secondline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.secondline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.thirdline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.thirdline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.fourthline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.fourthline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				if (i * 2 < 12) KeyManager.Instance.fourthline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.fourthline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.thirdline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.thirdline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.secondline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.secondline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);
				if (i * 2 < 12) KeyManager.Instance.firstline[i * 2].DamageEvent(time, duration, true, "ThunderPulse");
				if (i * 2 + 1 < 12) KeyManager.Instance.firstline[i * 2 + 1].DamageEvent(time, duration, true, "ThunderPulse");
				yield return new WaitForSeconds(0.1f);				
			}
			yield return new WaitForSeconds(0.4f);				
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private void SliceDrop(int num, float time, float duration)
	{
		if(num % 2 == 0) bossMain.SetAnimation("AttackSliceL");
		else if(num % 2 == 1) bossMain.SetAnimation("AttackSliceR");
		for (int i = num; i < 12; i += 2)
		{
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "Thunder2");
			KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "Thunder2");
		}
	}

	private IEnumerator SliceThunder(float time, float duration, int count = 2, float waitTime = 1f)
	{
		for (int i = 0; i < count; i++)
		{
			SliceDrop(i % 2, time, duration);
			yield return new WaitForSeconds(0.7f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator ThunderBurst(float time, float duration, float waitTime = 1f)
	{
		bossMain.SetAnimation("AttackBurst");
		if (PassiveCoroutine != null)
		{
			StopCoroutine(PassiveCoroutine);
			PassiveCoroutine = null;
		}
		int thunderNum;
		string particleName;
		for (int i = 0; i < 6; i++)
		{
			thunderNum = Random.Range(1, 4);
			particleName = "Thunder" + thunderNum.ToString();
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, particleName);
			KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, particleName);
			if(i <= 11 - i)KeyManager.Instance.firstline[11 - i].DamageEvent(time, duration, true, particleName);
			if(i <= 10 - i)KeyManager.Instance.secondline[10 -i].DamageEvent(time, duration, true, particleName);
			yield return new WaitForSeconds(0.2f);
		}
		for (var i = RowKey.A; i <= RowKey.Semicolon; i++)
		{
			KeyManager.Instance.MainBoard[(int)i].DamageEvent(time, duration, true, "Thunder1");
			KeyManager.Instance.MainBoard[(int)RowKey.Semicolon + (int)RowKey.A - (int)i].DamageEvent(time, duration, true, "Thunder1");
			yield return new WaitForSeconds(0.1f);
		}
		KeyManager.Instance.fourthline.ForEach(keyBase =>
		{
			keyBase.DamageEvent(0.7f, 0.2f, true, "ThunderPulse");
		});
		yield return new WaitForSeconds(0.5f);
		KeyManager.Instance.thirdline.ForEach(keyBase =>
		{
			keyBase.DamageEvent(0.7f, 0.2f, true, "ThunderPulse");
		});
		yield return new WaitForSeconds(0.5f);
		KeyManager.Instance.secondline.ForEach(keyBase =>
		{
			keyBase.DamageEvent(0.7f, 0.2f, true, "ThunderPulse");
		});
		yield return new WaitForSeconds(waitTime);
		if(PassiveCoroutine == null) UpdatePattern();
		ChangePattern();
	}

	#endregion

}
