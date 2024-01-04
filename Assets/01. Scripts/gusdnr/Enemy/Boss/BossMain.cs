using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMain : MonoBehaviour
{
	[Header("Components")]
	public BossHPController HPController;
	[SerializeField] private Animator BossAnimator;
	private BossPatternBase BossPatternRunner;
	private Transform target;


		[Header("Objects")]
	[SerializeField] private GameObject BossBackground;

	[Header("Values")]
	public Vector3 KeyboardPos;
	public int NextBossNum;
	public float MaxHP;
	private float curHP;
	public float CurHP
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
		HPController = GetComponent<BossHPController>();
		BossPatternRunner = GetComponent<BossPatternBase>();
		target = transform.Find("target").GetComponent<Transform>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GetDamage(10);
		}
	}

	public void StartBossPattern()
	{
		StartCoroutine(SettingBoss());
	}

	private IEnumerator SettingBoss()
	{
		curHP = MaxHP;
		BossBackground.SetActive(true);
		GameManager.Instance.Keyboard.transform.position = KeyboardPos;
		GameManager.Instance.Fade.SetTrigger("FadeOut");
		yield return new WaitForSeconds(2f);
		GameManager.Instance.Target = target;
		yield return new WaitForSeconds(1f);
		BossPatternRunner.StartPattern();
	}

	public void GetDamage(float Damage)
	{
		curHP -= Damage;
		HPController.SetFillAmount();
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
		StartCoroutine(BackgroundActiveFalse(3));
		GameManager.Instance.SetBoss(NextBossNum);
	}

	private IEnumerator BackgroundActiveFalse(float time)
	{
		yield return new WaitForSeconds(time);
		BossBackground.SetActive(false);
	}
}
