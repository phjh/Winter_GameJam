using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoBullet : PoolableMono
{
	public LayerMask WhatIsEnemy;
	GameObject target;
	public float Speed = 3;

	private void Awake()
	{
		Init();
	}


	public override void Init()
	{
		//target = GameObject.Find("Target");
		//Rigidbody2D rb = GetComponent<Rigidbody2D>();

		//rb.velocity = (target.transform.position - transform.position).normalized * Speed;
		//transform.LookAt(target.transform.position);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PoolManager.Instance.Push(this);
	}

}
