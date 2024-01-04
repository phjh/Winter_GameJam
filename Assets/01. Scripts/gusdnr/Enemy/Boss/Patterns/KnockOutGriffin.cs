using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutGriffin : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] explosionParticle;
	public GameObject Parent;
	
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		if(animator == null) animator = GetComponent<Animator>();
		animator.SetBool("die", true);
	}

	public void ExplosionParticlePlay()
	{
		int rand = Random.Range(0, explosionParticle.Length);
		if (!explosionParticle[rand].isPlaying)
		{
			explosionParticle[rand].Play();
		}
	}

	public void DisAbleGriffin()
	{
		animator.SetBool("die", false);
		Parent.SetActive(false);
	}
}
