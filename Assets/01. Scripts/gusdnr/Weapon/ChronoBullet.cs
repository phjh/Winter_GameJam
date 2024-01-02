using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoBullet : PoolableMono
{
	public LayerMask WhatIsEnemy;
	

	private void Awake()
	{
		Init();
	}


	public override void Init()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PoolManager.Instance.Push(this);
	}

}
