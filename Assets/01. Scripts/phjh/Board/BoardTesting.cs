using System.Collections;
using System.Globalization;
using UnityEngine;

public class BoardTesting : MonoBehaviour
{
    [SerializeField]
    float time;
    [SerializeField]
    float duration;

    private IEnumerator AttackCoroutine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(AttackCoroutine == null)
            {
			    //AttackCoroutine = ContinualAttack(RowKey.Q, RowKey.Semicolon, 0.2f);
                AttackCoroutine = BetweenAttack(RowKey.Q, RowKey.Y, RowKey.Leftbasket, 0.5f);
			   StartCoroutine(AttackCoroutine);
            }
			/*
			for (var i = RowKey.one; i <= RowKey.equal; i++)  //여기서 범위잡아주고
			{//요안에서 조건쓰면됨
				KeyManager.Instance.Boards[(int)i].DamageEvent(3, time, duration);
			}
			for (var i = RowKey.A; i <= RowKey.Semicolon; i++)  //여기서 범위잡아주고
            {//요안에서 조건쓰면됨
                KeyManager.Instance.Boards[(int)i].DamageEvent(3,time,duration);
            }
            */
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (var i = RowKey.A; i < RowKey.Semicolon; i++)
            {
                KeyManager.Instance.MainBoard[(int)i].gameObject.SetActive(true);
                KeyManager.Instance.AddConnectKeys(KeyManager.Instance.MainBoard[(int)i]);
            }
        }
    }


    IEnumerator ContinualAttack(RowKey StartKey, RowKey EndKey, float time)
    {
        for(var i = StartKey; i <= EndKey; i++)
        {
		    KeyManager.Instance.MainBoard[(int)i].DamageEvent(3, time, duration);
		    KeyManager.Instance.MainBoard[(int)EndKey +    (int)StartKey - (int)i].DamageEvent(3, time, duration);
            yield return new WaitForSeconds(time);
        }
		AttackCoroutine = null;
	}

	IEnumerator BetweenAttack(RowKey StartKey, RowKey MiddleKey, RowKey EndKey, float time)
	{
		for (var i = StartKey; i <= MiddleKey; i++)
		{
			KeyManager.Instance.MainBoard[(int)i].DamageEvent(3, time, duration);
			KeyManager.Instance.MainBoard[(int)EndKey + (int)StartKey - (int)i].DamageEvent(3, time, duration);
			yield return new WaitForSeconds(time);
		}
		AttackCoroutine = null;
	}

    /*IEnumerator LineAttack(RowKey Start1, RowKey Start2, RowKey Start3, float time)
    {
		for (var i = StartKey; i <= MiddleKey; i++)
		{
			KeyManager.Instance.Boards[(int)i].DamageEvent(3, time, duration);
			KeyManager.Instance.Boards[(int)EndKey + (int)StartKey - (int)i].DamageEvent(3, time, duration);
			yield return new WaitForSeconds(time);
		}
	}*/
}
