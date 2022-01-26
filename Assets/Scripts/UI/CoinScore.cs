using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public int coins = 0;
    public Text CoinText;

    public int keys = 0;
    public Text KeysText;

    //variables que se muestran en la interfaz
    void Update()
    {
        coins = ObjSingleton.ObjS.Coins;
        keys = ObjSingleton.ObjS.Keys;
        if (coins !=null)
        {
            CoinText.text = coins + "/8";
        }
        if (keys != null)
        {
            KeysText.text = keys + "/2";
        }

    }
    //get manager llama a estas funciones cuando encuentra el item
    //AHORA SE HACE DESDE EL SINGLETON ObjSingleton
    //public void puntoCoins()
    //{
    //    coins = coins + 1;
    //}
    //public void puntoKeys()
    //{
    //    keys = keys + 1;
    //}
}
