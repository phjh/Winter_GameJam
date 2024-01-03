using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : PoolableMono
{
	private ParticleSystem attackParticle;
	private bool isOnParticle = false;

	private void Awake()
	{
		attackParticle = GetComponent<ParticleSystem>();
		isOnParticle = false;
	}

	private void Update()
	{
		if(isOnParticle && !attackParticle.isPlaying) PoolManager.Instance.Push(this);
	}

	public override void Init()
	{
		attackParticle.Play();
		isOnParticle = true;
	}
}
