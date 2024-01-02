using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoSingleton<KeyManager>
{
    public bool isFound = false;
    public KeyBase AimKey;
    public List<KeyBase> Boards;
    public SortedSet<KeyBase> set;
}
