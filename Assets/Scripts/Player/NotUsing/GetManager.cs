using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManager : MonoBehaviour
{
    //cuando se choca con un objeto destruye ese objeto y suma un +1 al objeto en la UI
    private void OnTriggerEnter(Collider collision)
    {
        //AHORA SE HACE DESDE EL SINGLETON ObjSingleton
         //if (collision.gameObject.tag == "coin")
         //{

         //    Destroy(collision.gameObject);
         //    GameObject.Find("Canvas").GetComponent<CoinScore>().puntoCoins();
         //}

        //if (collision.gameObject.tag == "Key")
        //{
        //    Destroy(collision.gameObject);
        //    GameObject.Find("Canvas").GetComponent<CoinScore>().puntoKeys();
        //}

    }
}
