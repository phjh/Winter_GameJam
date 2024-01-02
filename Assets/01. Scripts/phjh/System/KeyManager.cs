using System;
using System.Collections.Generic;
using UnityEngine;

public enum RowKey
{
    one=0, two, three, four, five, six, seven, eight, nine, zero,minus /* - */,equal,//=  (11)
    Q,W,E,R,T,Y,U,I,O,P,Leftbasket,//[ (22)
    A,S,D,F,G,H,J,K,L,Semicolon, //;  (32)
    Z,X,C,V,B,N,M,Comma/*,*/, Period//.   (41)
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
    public List<KeyBase> Boards;
    public GameObject DamageEffecter;
    //public Tuple<Sprite,Sprite> KeySprites;

    public void RefreshConnectKeys()
    {
        foreach (var key in Boards)
        {
            key.RefreshConnectedKey();
        }
    }


}
