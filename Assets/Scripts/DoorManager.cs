using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private CoinScore CoinScored;
    private Transform Doortr;
    public Transform Door2tr;

    public float speed = 5f;
    public Transform target;
    public Transform target2;
    // Start is called before the first frame update
    void Start()
    {
        Doortr = GetComponent<Transform>();
        CoinScored = GameObject.Find("Canvas").GetComponent<CoinScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CoinScored.keys == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (CoinScored.keys == 2)
        {
            Door2tr.position = Vector3.MoveTowards(Door2tr.position, target2.position, speed * Time.deltaTime);
        }
    }
}
