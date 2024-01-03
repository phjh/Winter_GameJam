using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffinPattern : BossPatternBase
{
	[SerializeField] private int MaxPatternValue;
	private BossMain bossMain;
	private IEnumerator AttackCoroutine;

	public List<GriffinOrb> griffinOrbs = new List<GriffinOrb>();

	private void Awake()
	{
		bossMain = GetComponent<BossMain>();
	}

	private int PatternNum = 0;
	private int TempPatternNum;
	public int OrbCount = 0;

	public override void StartPattern()
	{
		griffinOrbs.Clear();
	}

	public override void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false)
	{
		throw new System.NotImplementedException();
	}

	public override void OnDie()
	{
		AttackCoroutine = null;
		StopAllCoroutines();
		BossPatternBase bossPattern = this;
		bossPattern.enabled = false;
	}

	

	public override void UpdatePattern()
	{
		throw new System.NotImplementedException();
	}
}
