using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAlarmController : MonoSingleton<LineAlarmController>
{
    [SerializeField] private ParticleSystem[] LeftAlarm;
    [SerializeField] private ParticleSystem[] RightAlarm;

	private void Start()
	{
		foreach (var item in LeftAlarm)
        {
            item.Clear();
        }
		foreach (var item in RightAlarm)
		{
			item.Clear();
		}
	}

	public void ActiveAlarm(int lineNum)
    {
        LeftAlarm[lineNum - 1].Play();
        RightAlarm[lineNum - 1].Play();
    }
}
