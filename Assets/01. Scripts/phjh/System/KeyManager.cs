using System;
using System.Collections.Generic;
using UnityEngine;

public enum RowKey
{
    one=0, two, three, four, five, six, seven, eight, nine, zero,minus /* - */,equal,//=  (11)
    Q,W,E,R,T,Y,U,I,O,P,Leftbasket,//[ (22)
    A,S,D,F,G,H,J,K,L,Semicolon, //;  (32)
    Z,X,C,V,B,N,M,Comma/*,*/, Period, End//.   (42)
    ,LeftShift,RightShift,Space,
}

//public enum ColumnKey
//{
//    one=0,Q,A,Z,    two,W,S,X,     three,E,D,C,
//    four,R,F,V,     five,T,G,B,   six,Y,H,N,
//    seven,U,J,M,    eight,I,K,Comma,    nine,O,L,Period,
//    zero,P,Semicolon,   minus,leftbasket,   equal
//}

public class KeyManager : MonoSingleton<KeyManager>
{
    //public bool isFound = false;
    //public KeyBase AimKey;
    //public SortedSet<KeyBase> set;
    public List<KeyBase> MainBoard;
    public List<KeyBase> firstline;
    public List<KeyBase> secondline;
    public List<KeyBase> thirdline;
    public List<KeyBase> fourthline;
    public GameObject DamageEffecter;

    public Material immunityMat;

	private void Start()
	{
		for (int i = 0; i < MainBoard.Count; i++)
		{
			MainBoard[i].BaseKey = (RowKey)i;
		}
	}

	public void AddConnectKeys(KeyBase key) //킬때는 이거
    {
        key.AddConnectedKey();
    }
    
    public void DeleteConnectkeys(KeyBase key) //끌때는 이거
    {
        key.DeleteConnectedKey();
    }

}
