using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : PoolableMono
{
	private bool isEndAttack = false;
	private bool isHardCore;

	private void Awake()
	{
		isHardCore = GameManager.Instance.IsHardCore;
	}

	public override void Init()
	{
		isEndAttack = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isEndAttack == false)
		{
			if (collision.CompareTag("Player"))
			{
				isEndAttack = true;
				Player player = collision.gameObject.GetComponent<Player>();
				//player.CalculationHP(-1);
				Debug.Log($"{player.name} is hurt");
			}
		}
	}
}
