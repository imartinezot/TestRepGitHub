using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public int Damage;
    public float FuerzaBala;
    private GameObject Target;

    Rigidbody rb;
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        //rb.AddForce((transform.right *-1) * FuerzaBala);
        StartCoroutine(EspDest());
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, FuerzaBala);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().PlayerHealthState -= Damage;

            //se desactiva para luego poder seguir usandolo en el pool
            gameObject.SetActive(false);
        }
        //if (other.gameObject.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }

    IEnumerator EspDest()
    {
        yield return new WaitForSeconds(2f);
        //se desactiva para luego poder seguir usandolo en el pool
        gameObject.SetActive(false);
    }
}
