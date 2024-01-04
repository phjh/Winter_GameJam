using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	public LayerMask WhatIsEnemy;
	private Transform target;
	[SerializeField] private float Speed = 7;
	
	public float Damage {  get; set; }

	private void Awake()
	{
		Init();
	}

	public  void Init()
	{
		Destroy(this.gameObject, 3f);
		target = GameManager.Instance.Target;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		rb.velocity = (target.position - transform.position).normalized * Speed;
		Vector2 dir = transform.position - target.position;
		transform.Rotate(0,0, (Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg));
	}


    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 6)
		{
			BossMain hitEnemy = collision.gameObject.GetComponent<BossMain>();
			hitEnemy.GetDamage(Damage);
			Destroy(this.gameObject);
		}
	}

}
