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
            for(var i = RowKey.A; i < RowKey.Semicolon; i++)  //���⼭ ��������ְ�
            {//��ȿ��� ���Ǿ����
                if((int)i%2 == 0)
                {
                    KeyManager.Instance.Boards[(int)i].DamageEvent(3,time,duration);
                }
            }
            KeyManager.Instance.RefreshConnectKeys();
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (var i = RowKey.A; i < RowKey.Semicolon; i++)
            {
                KeyManager.Instance.Boards[(int)i].gameObject.SetActive(true);
            }
            KeyManager.Instance.RefreshConnectKeys();
        }
    }

}
