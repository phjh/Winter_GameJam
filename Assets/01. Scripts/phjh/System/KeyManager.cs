using System.Collections.Generic;

public enum RowKey
{
    one=0, two=1, three=2, four=3, five=4, six=5, seven=6, eight=7, nine=8, zero=9,minus = 10 /* - */,equal=11,//=
    Q,W,E,R,T,Y,U,I,O,P,Leftbasket,//[
    A,S,D,F,G,H,J,K,L,Semicolon, //;
    Z,X,C,V,B,N,M,Comma/*,*/, Period//.
}

public enum ColumnKey
{
    one=0,Q,A,Z,    two,W,S,X,     three,E,D,C,
    four,R,F,V,     five,T,G,B,   six,Y,H,N,
    seven,U,J,M,    eight,I,K,Comma,    nine,O,L,Period,
    zero,P,Semicolon,   minus,leftbasket,   equal


}


public class KeyManager : MonoSingleton<KeyManager>
{
    //public bool isFound = false;
    //public KeyBase AimKey;
    //public SortedSet<KeyBase> set;
    public List<KeyBase> Boards;

    public void RefreshConnectKeys()
    {
        foreach (var key in Boards)
        {
            key.RefreshConnectedKey();
        }
    }


}
