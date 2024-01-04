using DG.Tweening;
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
	[SerializeField] private GameObject bullet;
	public Transform target;
	private bool isMoving = false;
	private float time;
	private float attackTime = 0.2f;
	[SerializeField]
	private float attackCooltime=0.1f;
	bool isleft = true;
	SpriteRenderer playerSP;


	//플레이어 스크립트 내 밸류
	[HideInInspector] public float PlayerInt;
	[HideInInspector] public float PlayerMoveSpeed;
	[HideInInspector] public float PlayerCurHealth;
	[HideInInspector] public float PlayerDamage;
	[HideInInspector] public float PlayerAtkSpeed;
	[HideInInspector] public float PlayerCriticChance;

	private void Awake()
	{
		playerSP = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		if(target == null)
		{
			if(GameManager.Instance.Target == null)
			{
				Debug.LogError("타겟이 존재하지 않습니다.");
			}
			target = GameManager.Instance.Target;
		}
	}

	private void Update()
	{
		isMoving = GetComponentInParent<PlayerMove>().isMoving;
		FlipControl(ChronoParent);
		FlipControl(BoardParent);
		Attack();
		playerSP.flipX = (bool)(transform.position.x > target.transform.position.x);

    }

	private void FlipControl(GameObject FlipObject)
	{
		if (transform.position.x > target.transform.position.x)
		{
			FlipObject.transform.localScale = new Vector3(1, 1, 1);
		}
		else if (transform.position.x < target.transform.position.x)
		{
			FlipObject.transform.localScale = new Vector3(1, -1, 1);
		}

		Vector2 dir = FlipObject.transform.position - target.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		FlipObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void Attack()
	{
		if(isMoving == false)
		{
			time += Time.deltaTime;
			if(attackTime > 5)
			{
				PlayerMove playerMove = transform.GetComponentInParent<PlayerMove>();
				StartCoroutine(playerMove.CorruptedKey());
				attackTime = attackCooltime;
				time = 0;
				//대충 여기에 오염 그거 만들기
			}
			else if(time > attackTime)
			{
				attackTime += attackCooltime;

				if (isleft)
					CreateBullet(CFirePos.gameObject);
				else
					CreateBullet(BFirePos.gameObject);
				
				isleft = !isleft;
				//ChronoBullet bullet = PoolManager.Instance.Pop("Bullet") as ChronoBullet;
				//bullet.transform.position = transform.position;
				//PoolManager.Instance.Push(bullet);
            }
		}
		else 
		{
			time = 0;
			attackTime = attackCooltime;
		}
	}

	void CreateBullet(GameObject obj)
	{
		Instantiate(bullet, obj.transform.position, Quaternion.identity);
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
