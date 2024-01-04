using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	[Header("난이도(추후 추가 예정)")]
    private bool isHardCore;
    public bool IsHardCore => isHardCore;

	public Transform Target { get; set; }
	public KeyBase PlayerPos { get; set; }
	public GameObject Keyboard;
	public Animator Fade;

	[SerializeField]
	private PoolingListSO _poolingList;

	[SerializeField] private BossMain[] Bosses;

	private void Awake()
	{
		CreatePool();
	}

	private void Start()
	{
		isHardCore = false;
		SetBoss(0, true);
	}

	public void SetBoss(int bossNum, bool isStart = false)
	{
		if (bossNum == 4) Application.Quit();
		StartCoroutine(Setting(bossNum, isStart));
	}

	private IEnumerator Setting(int bossNum, bool isStart = false)
	{
		if(isStart == false)Fade.SetTrigger("FadeIn");
		yield return new WaitForSeconds(3f);
		Bosses[bossNum].gameObject.SetActive(true);
		Bosses[bossNum].StartBossPattern();
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
