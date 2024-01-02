using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
	public abstract void ChangePattern();
	public abstract void SetPattern();
	public abstract void UpdatePattern();
}
