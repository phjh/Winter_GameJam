using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
	public abstract void StartPattern(); //보스전 시작시 해당 함수 호출
	public abstract void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false); //패턴 계속 바꿔주기, 코루틴 끝에 필수로 호출해줘야함
	public abstract void UpdatePattern(); //지속 공격 같은 것에 사용하기 위해 선언해둠
	public abstract void OnDie(); //보스가 처치되었을 경우 호출
}
