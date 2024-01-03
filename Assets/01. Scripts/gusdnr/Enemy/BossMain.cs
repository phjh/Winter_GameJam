using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
	//private IEnumerator AttackCoroutine;

	[SerializeField] private PoolingListSO BossParticles;

	private Animator BossAnimator;


	private void Awake()
	{
		BossAnimator = GetComponent<Animator>();
	}

	private void Start()
	{
		BossParticles.Pairs.ForEach(pair => 
		{
			PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
		});
		
	}

	private void Update()
	{


	}
}
