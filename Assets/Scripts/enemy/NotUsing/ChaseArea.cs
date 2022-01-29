using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArea : MonoBehaviour
{
    //AHORA SE HACE DESDE OTRO SCRIPT
    public GameObject ChaseAreas;
    public GameObject enemigo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==("Detection"))
        {
            enemigo.GetComponent<Chase>().enemyAI = true;
            ChaseAreas.SetActive(false);
        }

    }
}
