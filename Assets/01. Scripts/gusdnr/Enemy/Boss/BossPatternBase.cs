using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
	public abstract void StartPattern(); //������ ���۽� �ش� �Լ� ȣ��
	public abstract void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false); //���� ��� �ٲ��ֱ�, �ڷ�ƾ ���� �ʼ��� ȣ���������
	public abstract void UpdatePattern(); //���� ���� ���� �Ϳ� ����ϱ� ���� �����ص�
	public abstract void OnDie(); //������ óġ�Ǿ��� ��� ȣ��
}
