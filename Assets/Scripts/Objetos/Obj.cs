using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj : MonoBehaviour
{
    [SerializeField] ObjectData Data;


    //Cuando el objeto que sea detecta que ha chocado con el player hace la funcion
    //correspondiente de el tipo de objeto que es, ej: si es coin suma un coin a la interfaz
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.Coin)
        {
            //suma 1 coin
            ObjSingleton.ObjS.Coin1();
            Destroy(this.gameObject);
        } 
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.HealthPotion)
        {
            //suma +50 de vida al personaje
            other.gameObject.GetComponent<PlayerHealth>().PlayerHealthState += 50;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player" && Data.Tipo == ObjectData.TIPO.Key)
        {
            //suma 1 llave
            ObjSingleton.ObjS.Key1(); //suma +1 key
            Destroy(this.gameObject);
        }
    }
}
