using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

	[Header("스텟")]
	public float PlayerDamage = 5;
	public float PlayerMaxHealth = 6;
	public float PlayerCurHealth { get; set; }
	/*
	[SerializeField] private CharacterStat _characterStat;
	public CharacterStat Stat => _characterStat;*/

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

	[SerializeField]
	CinemachineImpulseSource _impulse;
    [SerializeField]
    float amplitude = 1.7f;


	//플레이어 스크립트 내 밸류


	private void Awake()
	{
		playerSP = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		PlayerCurHealth = PlayerMaxHealth;
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
		if(target != null) Attack();

		if(target == null)
		{
			target = GameManager.Instance.Target;
		}
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
				//오지홍 <- 이 새끼 메모리 생각한다면서 여기서 줄줄 새는 중
				attackTime += attackCooltime;

				if (isleft)
				{
					PlayerBullet cb = PoolManager.Instance.Pop("ChronoBullet") as PlayerBullet;
					cb.transform.position = CFirePos.position;
					cb.Damage = PlayerDamage;
				}
				else
				{
					PlayerBullet bb = PoolManager.Instance.Pop("BoardBullet") as PlayerBullet;
					bb.transform.position = BFirePos.position;
					bb.Damage = PlayerDamage;
				}
				
				isleft = !isleft;
				_impulse.GenerateImpulse(amplitude);

				//ChronoBullet bullet = PoolManager.Instance.Pop("Bullet") as ChronoBullet;
				//bullet.transform.position = transform.position;
				//PoolManager.Instance.Push(bullet);
            }
			else
			{
				_impulse.GenerateImpulse(0);
			}
		}
		else 
		{
			time = 0;
			attackTime = attackCooltime;
		}
	}

	public void CalculationHP(int value, bool isHardCore = false)
	{
		if(isHardCore && value < 0) Die();
		PlayerCurHealth += value;
		Mathf.Clamp(PlayerCurHealth, 0, PlayerMaxHealth);
		if(PlayerCurHealth <= 0) Die();
	}

	private void Die()
	{
		//죽었을 때 실행할 것들 넣기
	}
}
