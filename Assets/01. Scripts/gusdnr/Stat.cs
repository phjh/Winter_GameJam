using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
	[SerializeField] private int _baseValue;

	public List<int> modifiers = new List<int>();

	public int GetValue()
	{
		int finalValue = _baseValue;
		for (int i = 0; i < modifiers.Count; ++i)
		{
			finalValue += modifiers[i];
		}
		return finalValue;
	}

	public void SetDefaultValue(int value)
	{
		_baseValue = value;
	}

	public void ResetValue() //기본 값으로 돌리기 위해 해당 스텟에 존재하는 모든 Modifer 제거
	{
		if (modifiers.Count > 0)
		{
			foreach (int modifier in modifiers)	RemoveModifier(modifier);
		}
	}

	public void AddModifier(int value)
	{
		if (value != 0)
			modifiers.Add(value);
	}

	public void RemoveModifier(int value)
	{
		if (value != 0)
			modifiers.Remove(value);
	}
}
