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

	public void ResetValue() //기본 값으로 돌리기 위해 해당 스텟에 존재하는 모든 Modifer 제거
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
