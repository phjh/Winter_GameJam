using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum StatType
{
	intelligence,
	agility,
	maxHealth,
	damage,
	atkSpeed,
	criticalDamage,
	criticalChance
}

[CreateAssetMenu(menuName = "SO/Stat")]
public class CharacterStat : ScriptableObject
{
	[Header("주요 스텟")]
	public Stat intelligence;
	public Stat agility;
	public Stat maxHealth;

	[Header("공격 스탯")]
	public Stat damage;
	public Stat atkSpeed;
	public Stat criticalDamage;
	public Stat criticalChance;

	protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary;

	protected Player _owner;
	public void SetOwner(Player owner)
	{
		_owner = owner;
	}

	public void IncreaseStatBy(int modifyValue, float duration, StatType statType)
	{
		_owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statType));
	}

	protected IEnumerator StatModifyCoroutine(int modifyValue, float duration, StatType statType)
	{
		Stat target = GetStatByType(statType);
		target.AddModifier(modifyValue);
		yield return new WaitForSeconds(duration);
		if(duration != -1) target.RemoveModifier(modifyValue);
	}

	protected void OnEnable()
	{
		if (_fieldInfoDictionary == null)
		{
			_fieldInfoDictionary = new Dictionary<StatType, FieldInfo>();
		}
		_fieldInfoDictionary.Clear();

		Type characterStatType = typeof(CharacterStat);
		foreach (StatType statType in Enum.GetValues(typeof(StatType)))
		{
			FieldInfo statField = characterStatType.GetField(statType.ToString());

			if (statField == null)
			{
				Debug.LogError($"There are no stat! error : {statType.ToString()}");
			}
			else
			{
				_fieldInfoDictionary.Add(statType, statField);
			}
		}
	}

	public void ResetStats() //게임 재 시작시 모든 스텟 증가 제거 함수
	{
		foreach (StatType statType in Enum.GetValues(typeof(StatType)))
		{
			Stat target = GetStatByType(statType);
			target.ResetValue();
		}
	}

	public Stat GetStatByType(StatType type)
	{
		return _fieldInfoDictionary[type].GetValue(this) as Stat;
	}
}
