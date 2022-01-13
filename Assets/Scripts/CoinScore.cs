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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coins !=null)
        {
            CoinText.text = coins + "/8";
        }
        if (keys != null)
        {
            KeysText.text = keys + "/2";
        }

    }
    public void puntoCoins()
    {
        coins = coins + 1;
    }
    public void puntoKeys()
    {
        keys = keys + 1;
    }
}
