using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    //private CoinScore CoinScored;
    public Transform Door2tr;

    public float speed = 5f;
    public Transform target;
    public Transform target2;
    void Start()
    {
        //CoinScored = GameObject.Find("Canvas").GetComponent<CoinScore>();
    }


    //cuando se consigue una llave la puerta correspondiente se va hacia arriba y se abre
    void Update()
    {
        if (ObjSingleton.ObjS.Keys == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (ObjSingleton.ObjS.Keys == 2)
        {
            Door2tr.position = Vector3.MoveTowards(Door2tr.position, target2.position, speed * Time.deltaTime);
        }
    }
}
