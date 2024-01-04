using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GriffinPattern : BossPatternBase
{
	[SerializeField] private int MaxPatternValue;
	private BossMain bossMain;
	private IEnumerator AttackCoroutine;

	public GameObject Visual;
	public GameObject KnockOut;

	private void Awake()
	{
		bossMain = GetComponent<BossMain>();
		KnockOut.SetActive(false);
	}

	private int PatternNum = 0;
	private int TempPatternNum;

	public override void StartPattern()
	{

		if (AttackCoroutine != null)
		{
			Debug.LogError("패턴 코루틴이 초기화되지 않았습니다.");
		}
		if (AttackCoroutine == null)
		{
			ChangePattern();
		}
	}

	public override void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false)
	{
		AttackCoroutine = null;
		//bossMain.SetAnimation("open");
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
		switch (PatternNum)
		{
			case 0:
				AttackCoroutine = LineLaser();
				break;
			case 1:
				AttackCoroutine = OddLaser(0.5f, 0.3f);
				break;
			case 2:
				AttackCoroutine = HomingLaser(0.6f, 0.4f);
				break;
			case 3:
				AttackCoroutine = MultipleBoneAttack(0.7f, 0.3f);
				break;
			default:
				break;
		}
		StartCoroutine(AttackCoroutine);
	}

	public override void OnDie()
	{
		StopAllCoroutines();
		AttackCoroutine = null;
		Visual.SetActive(false);
		KnockOut.SetActive(true);
		BossPatternBase bossPattern = this;
		bossPattern.enabled = false;
	}

	public override void UpdatePattern()
	{
		//그리핀은 사용 X
	}

	#region Patterns

	private void LineAttacking(int num)
	{
		switch (num)
		{
			case 1:
				KeyManager.Instance.firstline.ForEach(keyBase =>
				{
					keyBase.DamageEvent(0.7f, 0.2f, true, "Laser");
				});
				break;
			case 2:
				KeyManager.Instance.secondline.ForEach(keyBase =>
				{
					keyBase.DamageEvent(0.7f, 0.2f, true, "Laser");
				});
				break;
			case 3:
				KeyManager.Instance.thirdline.ForEach(keyBase =>
				{
					keyBase.DamageEvent(0.7f, 0.2f, true, "Laser");
				});
				break;
			case 4:
				KeyManager.Instance.fourthline.ForEach(keyBase =>
				{
					keyBase.DamageEvent(0.7f, 0.2f, true, "Laser");
				});
				break;
		}
	}

	private IEnumerator LineLaser(float waitTime = 0.5f)
	{
		int random;
		for (int i = 0; i < 4; i++)
		{
			random = Random.Range(0, 6);
			switch (random)
			{
				case 0:
					LineAttacking(1);
					LineAttacking(2);
					break;
				case 1:
					LineAttacking(2);
					LineAttacking(3);
					break;
				case 2:
					LineAttacking(3);
					LineAttacking(4);
					break;
				case 3:
					LineAttacking(1);
					LineAttacking(3);
					break;
				case 4:
					LineAttacking(2);
					LineAttacking(4);
					break;
				case 5:
					LineAttacking(1);
					LineAttacking(4);
					break;
			}
			yield return new WaitForSeconds(1.3f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator HomingLaser(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i < 5; i++)
		{
			GameManager.Instance.PlayerPos.DamageEvent(time, duration, true, "Laser");
			yield return new WaitForSeconds(0.6f);
		}
		//bossMain.SetAnimation("close");
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}

	private IEnumerator OddLaser(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i <= 11; i++)
		{
			KeyManager.Instance.firstline[i].DamageEvent(time, duration, true, "Laser");
			KeyManager.Instance.secondline[i].DamageEvent(time, duration, true, "Laser");
			KeyManager.Instance.thirdline[i].DamageEvent(time, duration, true, "Laser");
			KeyManager.Instance.fourthline[i].DamageEvent(time, duration, true, "Laser");
			yield return new WaitForSeconds(0.7f);
		}
		//bossMain.SetAnimation("close");
		yield return new WaitForSeconds(waitTime);
		ChangePattern(3, true);
	}

	private void BoneAttack(KeyBase center, float time, float duration)
	{
		//커넥트 키 0,1, 4,5 칸 공격
		center.DamageEvent(time, duration, true, "Laser");
		if(center.connectedKeys[0] != null)center.connectedKeys[0].DamageEvent(time, duration, true, "Laser");
		if(center.connectedKeys[1] != null)center.connectedKeys[1].DamageEvent(time, duration, true, "Laser");
		if(center.connectedKeys[4] != null)center.connectedKeys[4].DamageEvent(time, duration, true, "Laser");
		if(center.connectedKeys[5] != null)center.connectedKeys[5].DamageEvent(time, duration, true, "Laser");
	}

	private IEnumerator MultipleBoneAttack(float time, float duration, float waitTime = 0.5f)
	{
		for (int i = 0; i < 4; i++)
		{
			if(i % 2 == 0) BoneAttack(KeyManager.Instance.secondline[Random.Range(1, 10)], time, duration);
			else BoneAttack(KeyManager.Instance.thirdline[Random.Range(1, 9)], time, duration);
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(waitTime);
		ChangePattern();
	}
	#endregion

}
