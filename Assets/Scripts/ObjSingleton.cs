using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjSingleton
{
    private static ObjSingleton objShared;

    //lista de objetos, llaves, monedas, pociones, puertas
    private List<GameObject> objs = new List<GameObject>();

    //acceso
    public List<GameObject> Objs { get{ return Objs; } }

    //variables de objetos en inventario
    private int _keys;
    private int _coins;

    public int Keys { get { return _keys; } }
    public int Coins { get { return _coins; } }

    public static ObjSingleton ObjS
    {
        get
        {
            if (objShared == null)
            {
                objShared = new ObjSingleton();
                objShared.objs.AddRange(GameObject.FindGameObjectsWithTag("Obj"));
            }
            return objShared;
        }
    }
    public void Coin1()
    {
        _coins += 1;
    }
    public void Key1()
    {
        _keys += 1;
    }
}
