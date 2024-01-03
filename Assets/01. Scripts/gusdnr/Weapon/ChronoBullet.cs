using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoBullet : PoolableMono
{
	public LayerMask WhatIsEnemy;
	GameObject target;
	public float Speed = 3;

	public ChronoBullet(int dmg, int critChance, int critDamage)
	{

	}

	private void Awake()
	{
		Init();
	}

	public override void Init()
	{
		target = GameObject.Find("Target");
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = (target.transform.position - transform.position).normalized * Speed;
		Vector2 dir = transform.position - target.transform.position;
		transform.Rotate(0,0, (Mathf.Atan2(target.transform.position.y-transform.position.y,target.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == WhatIsEnemy)
		{
			PoolManager.Instance.Push(this);
			Destroy(this);
		}
	}

}
