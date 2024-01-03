using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
	public abstract void ChangePattern(int LinkedPattern = -1, bool isFixedLink = false);
	public abstract void UpdatePattern();
}
