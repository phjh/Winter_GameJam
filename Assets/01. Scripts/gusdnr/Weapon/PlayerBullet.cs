using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : PoolableMono
{
	public LayerMask WhatIsEnemy;
	private Transform target;
	[SerializeField] private float Speed = 3;
	
	public float Damage {  get; set; }

	private void Awake()
	{
		Init();
	}

	public override void Init()
	{
		target = GameManager.Instance.Target;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = (target.position - transform.position).normalized * Speed;
		Vector2 dir = transform.position - target.position;
		transform.Rotate(0,0, (Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg) + 90);
		Invoke("AutoPush", 5f);
	}

	private void AutoPush()
	{
		PoolManager.Instance.Push(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == WhatIsEnemy)
		{
			BossMain hitEnemy = collision.gameObject.GetComponent<BossMain>();
			hitEnemy.GetDamage(Damage);
			PoolManager.Instance.Push(this);
		}
	}

}
