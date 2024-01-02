using System.Collections;
using System.Collections.Generic;
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
                int AttackNum = Random.Range(1,5);
                switch (AttackNum)
                {
                    case 1:
			            AttackCoroutine = ContinualAttack(RowKey.Q, RowKey.Semicolon, 0.2f);
                        break;
                    case 2:
                        AttackCoroutine = BetweenAttack(RowKey.Q, RowKey.Y, RowKey.Leftbasket, 0.5f);
                        break;
                    case 3:
                        AttackCoroutine = WaveAttack(0, 11, 0.4f);
                        break;
                    case 4:
                        AttackCoroutine = StampAttack(0.3f);
                        break;

                }
			   StartCoroutine(AttackCoroutine);
            }
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

    IEnumerator WaveAttack(int start, int distance, float time)
    {
        int nonArea = Random.Range(1, 5);
		for (var i = start; i <= start + distance; i++)
		{
            if(i > KeyManager.Instance.firstline.Count) break;
			if(nonArea != 1)KeyManager.Instance.firstline[(int)i].DamageEvent(3, time, 0.3f);
            if(nonArea != 2 && i < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[(int)i].DamageEvent(3, time, 0.3f);
            if(nonArea != 3 && i < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[(int)i].DamageEvent(3, time, 0.3f);
            if(nonArea != 4 && i < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[(int)i].DamageEvent(3, time, 0.3f);
			yield return new WaitForSeconds(time);
		}
		AttackCoroutine = null;
	}

	IEnumerator StampAttack(float time, int combo = 0)
	{
        switch (combo)
        {
            case 0:
				KeyManager.Instance.MainBoard[(int)RowKey.two].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.three].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.Q].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.W].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.E].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.A].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.S].DamageEvent(3, 0.7f, duration);
                yield return new WaitForSeconds(time);
                AttackCoroutine = StampAttack(time, combo + 1);
		        StartCoroutine(AttackCoroutine);
				break;
            case 1:
				KeyManager.Instance.MainBoard[(int)RowKey.R].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.T].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.D].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.F].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.G].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.C].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.V].DamageEvent(3, 0.7f, duration);
                yield return new WaitForSeconds(time);
                AttackCoroutine = StampAttack(time, combo + 1);
		        StartCoroutine(AttackCoroutine);
                break;
            case 2:
				KeyManager.Instance.MainBoard[(int)RowKey.eight].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.nine].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.U].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.I].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.O].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.J].DamageEvent(3, 0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.K].DamageEvent(3, 0.7f, duration);
				yield return new WaitForSeconds(time);
				AttackCoroutine = StampAttack(time, combo + 1);
				StartCoroutine(AttackCoroutine);
                break;
            case 3:
				AttackCoroutine = null;
				break;
		}
	}
}
