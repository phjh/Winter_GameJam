using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTesting : MonoBehaviour
{
    [SerializeField]
    float time;
    [SerializeField]
    float duration;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			for (var i = RowKey.one; i <= RowKey.equal; i++)  //���⼭ ��������ְ�
			{//��ȿ��� ���Ǿ����
                KeyManager.Instance.firstline[(int)i].DamageEvent(3, time, duration);
                KeyManager.Instance.secondline[(int)i].DamageEvent(3, time, duration);
                KeyManager.Instance.thirdline[(int)i].DamageEvent(3, time, duration);
                KeyManager.Instance.fourthline[(int)i].DamageEvent(3, time, duration);
            }
			for (var i = RowKey.A; i <= RowKey.Semicolon; i++)  //���⼭ ��������ְ�
            {//��ȿ��� ���Ǿ����
                //KeyManager.Instance.Boards[(int)i].DamageEvent(3,time,duration);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (var i = RowKey.A; i < RowKey.Semicolon; i++)
            {
                //KeyManager.Instance.Boards[(int)i].gameObject.SetActive(true);
            }
        }
    }

}
