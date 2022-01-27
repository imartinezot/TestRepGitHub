using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj : MonoBehaviour
{
    [SerializeField] ObjectData Data;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.Coin)
        {
            //acceder a singleton de UI
            ObjSingleton.ObjS.Coin1();
            Destroy(this.gameObject);
        } 
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.HealthPotion)
        {
            //acceder a singleton de UI
            other.gameObject.GetComponent<PlayerHealth>().PlayerHealthState += 50;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.Key)
        {
            //acceder a singleton de UI
            ObjSingleton.ObjS.Key1(); //suma +1 key
            Destroy(this.gameObject);
        }
    }
}
