using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

	[Header("스텟")]
	[SerializeField] private CharacterStat _characterStat;
	public CharacterStat Stat => _characterStat;

	[Header("공격")]
	[SerializeField] private GameObject ChronoParent;
	[SerializeField] private Transform CFirePos;
	[SerializeField] private GameObject BoardParent;
	[SerializeField] private Transform BFirePos;
	public Transform target;
	private bool isMoving = false;
	private float time;


	//플레이어 스크립트 내 밸류
	[HideInInspector] public float PlayerInt;
	[HideInInspector] public float PlayerMoveSpeed;
	[HideInInspector] public float PlayerCurHealth;
	[HideInInspector] public float PlayerDamage;
	[HideInInspector] public float PlayerAtkSpeed;
	[HideInInspector] public float PlayerCriticChance;

	private void Awake()
	{
		
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		FlipControl(ChronoParent);
		FlipControl(BoardParent);
		Attack();
	}

	private void FlipControl(GameObject FlipObject)
	{
		if (transform.position.x > 0)
		{
			FlipObject.transform.localScale = new Vector3(1, 1, 1);
		}
		else if (transform.position.x < 0)
		{
			FlipObject.transform.localScale = new Vector3(1, -1, 1);
		}

		Vector3 dir = FlipObject.transform.position - target.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		FlipObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void Attack()
	{
		int n = 1;
		if(isMoving == false)
		{
			time += Time.deltaTime;
			if(time >= 5)
			{
				//대충 여기에 오염 그거 만들기
			}
			else if(time >= n)
			{
				n++;
				//총알 날리기
			}
		}
		else 
		{
			time = 0;

		}
	}

	private void SetStat()
	{
		PlayerCurHealth = _characterStat.maxHealth.GetValue();
		PlayerInt = _characterStat.intelligence.GetValue();
		PlayerMoveSpeed = _characterStat.agility.GetValue();
		PlayerAtkSpeed = _characterStat.atkSpeed.GetValue();
		PlayerDamage = _characterStat.damage.GetValue();
		PlayerCriticChance = _characterStat.criticalChance.GetValue();
	}

	private void CalculationHP(int value, bool isHardCore = false)
	{
		if(isHardCore && value < 0) Die();
		PlayerCurHealth += value;
		if(PlayerCurHealth <= 0) Die();
	}

	private void Die()
	{
		//죽었을 때 실행할 것들 넣기
	}
}
