using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Animator BossAnimator;
	private BossPatternBase BossPatternRunner;
	private Transform target;

	[Header("Values")]
	public float MaxHP;
	public bool StartInMain = false;
	private float curHP;
	public float CuxHP
	{
		get
		{
			return curHP;
		}
		set
		{
			curHP = value;
		}
	}

	private void Awake()
	{
		BossAnimator = GetComponent<Animator>();
		BossPatternRunner = GetComponent<BossPatternBase>();
		target = transform.Find("target").GetComponent<Transform>();
	}

	private void Start()
	{
		//GameManager.Instance.Target = target;
		if(StartInMain) BossPatternRunner.StartPattern();
		curHP = MaxHP;
	}

	private void OnEnable()
	{
		GameManager.Instance.Target = target;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GetDamage(10);
		}
	}

	public void GetDamage(float Damage)
	{
		curHP -= Damage;
		if (curHP <= 0)
		{
			Die();
		}

	}

	public void SetAnimation(string TriggerName)
	{
		BossAnimator.SetTrigger(TriggerName);
	}

	private void Die()
	{
		GameManager.Instance.Target = null;
		BossPatternRunner.OnDie();
	}
}
