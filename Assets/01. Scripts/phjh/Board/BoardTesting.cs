using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
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
                int AttackNum = 5;
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
                    case 5:
                        AttackCoroutine = MineAttack(3);
                        break;
                }
			   StartCoroutine(AttackCoroutine);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (var i = RowKey.A; i < RowKey.Semicolon; i++)
            {
                //KeyManager.Instance.MainBoard[(int)RowKey.S].
                
                KeyManager.Instance.MainBoard[(int)i].gameObject.SetActive(true);
                KeyManager.Instance.AddConnectKeys(KeyManager.Instance.MainBoard[(int)i]);
            }
        }
    }


    IEnumerator ContinualAttack(RowKey StartKey, RowKey EndKey, float time)
    {
        for(var i = StartKey; i <= EndKey; i++)
        {
		    KeyManager.Instance.MainBoard[(int)i].DamageEvent(time, duration);
		    KeyManager.Instance.MainBoard[(int)EndKey +    (int)StartKey - (int)i].DamageEvent(time, duration);
            yield return new WaitForSeconds(time);
        }
		AttackCoroutine = null;
	}

	IEnumerator BetweenAttack(RowKey StartKey, RowKey MiddleKey, RowKey EndKey, float time)
	{
		for (var i = StartKey; i <= MiddleKey; i++)
		{
			KeyManager.Instance.MainBoard[(int)i].DamageEvent(time, duration);
			KeyManager.Instance.MainBoard[(int)EndKey + (int)StartKey - (int)i].DamageEvent(time, duration);
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
			if(nonArea != 1)KeyManager.Instance.firstline[(int)i].DamageEvent(time, 0.3f);
            if(nonArea != 2 && i < KeyManager.Instance.secondline.Count) KeyManager.Instance.secondline[(int)i].DamageEvent(time, 0.3f);
            if(nonArea != 3 && i < KeyManager.Instance.thirdline.Count) KeyManager.Instance.thirdline[(int)i].DamageEvent(time, 0.3f);
            if(nonArea != 4 && i < KeyManager.Instance.fourthline.Count) KeyManager.Instance.fourthline[(int)i].DamageEvent(time, 0.3f);
			yield return new WaitForSeconds(time);
		}
		AttackCoroutine = null;
	}

    IEnumerator MineAttack(float time)
    {
        int n = Random.Range((int)RowKey.one, (int)RowKey.Period + 1);
        KeyBase nowPos = KeyManager.Instance.MainBoard[n];
        nowPos.DamageEvent(1, 0.8f, true);
        foreach(var key in nowPos.connectedKeys)
        {
            key.DamageEvent(1, 0.8f, true);
        }
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(time / 4);
            n = Random.Range(0,KeyManager.Instance.MainBoard[n].connectedKeys.Count) % KeyManager.Instance.MainBoard[n].connectedKeys.Count;
            nowPos = nowPos.connectedKeys[i];
            nowPos.DamageEvent(1, 0.8f, true);
            foreach (var key in nowPos.connectedKeys)
            {
                key.DamageEvent(1, 0.8f, true);
            }
        }
        AttackCoroutine = null;
    }

    IEnumerator StampAttack(float time, int combo = 0)
	{
        switch (combo)
        {
            case 0:
				KeyManager.Instance.MainBoard[(int)RowKey.two].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.three].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.Q].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.W].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.E].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.A].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.S].DamageEvent(0.7f, duration);
                yield return new WaitForSeconds(time);
                AttackCoroutine = StampAttack(time, combo + 1);
		        StartCoroutine(AttackCoroutine);
				break;
            case 1:
				KeyManager.Instance.MainBoard[(int)RowKey.R].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.T].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.D].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.F].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.G].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.C].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.V].DamageEvent(0.7f, duration);
                yield return new WaitForSeconds(time);
                AttackCoroutine = StampAttack(time, combo + 1);
		        StartCoroutine(AttackCoroutine);
                break;
            case 2:
				KeyManager.Instance.MainBoard[(int)RowKey.eight].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.nine].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.U].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.I].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.O].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.J].DamageEvent(0.7f, duration);
				KeyManager.Instance.MainBoard[(int)RowKey.K].DamageEvent(0.7f, duration);
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
