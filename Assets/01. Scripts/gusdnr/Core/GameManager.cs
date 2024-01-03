using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("난이도(추후 추가 예정)")]
    private bool isHardCore;
    public bool IsHardCore => isHardCore;

	public Transform Target {  get; set; }

	[SerializeField]
	private PoolingListSO _poolingList;

	private void Awake()
	{
		CreatePool();
	}

	private void Start()
	{
		isHardCore = false;
	}

	private void CreatePool()
	{
		PoolManager.Instance = new PoolManager(transform);
		_poolingList.Pairs.ForEach(pair =>
		{
			PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
		});
	}
}
