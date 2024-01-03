using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
	[SerializeField] private float _baseValue;

	public List<float> modifiers = new List<float>();

	public float GetValue()
	{
		float finalValue = _baseValue;
		for (int i = 0; i < modifiers.Count; ++i)
		{
			finalValue += modifiers[i];
		}
		return finalValue;
	}

	public void SetDefaultValue(float value)
	{
		_baseValue = value;
	}

	public void ResetValue() //�⺻ ������ ������ ���� �ش� ���ݿ� �����ϴ� ��� Modifer ����
	{
		if (modifiers.Count > 0)
		{
			foreach (float modifier in modifiers)	RemoveModifier(modifier);
		}
	}

	public void AddModifier(float value)
	{
		if (value != 0)
			modifiers.Add(value);
	}

	public void RemoveModifier(float value)
	{
		if (value != 0)
			modifiers.Remove(value);
	}
}
