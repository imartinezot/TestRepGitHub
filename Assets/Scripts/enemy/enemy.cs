using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 50; //salud del enemigo
    public int Damage = 30; // daño que inflinge al player
    public HealthBar enemyHealthBar;

    public GameObject DisparoPos;
    public GameObject Bala;

    public bool EstadoDisparar;
    public bool disparar;

    public void Start()
    {
        enemyHealthBar.SetHealth(health);

    }
    public void Update()
    {
        enemyHealthBar.SetHealth(health);
        if (EstadoDisparar == true && disparar == true)
        {
            Disparar();
            disparar = false;
            StartCoroutine(WaitForShoot());
        }
    }
    //funcion llamada desde gun, le resta la vida establecida en el arma al enemigo
    public void TakeDamage (int amount)
    {
        health -= amount;
        if (health <=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    //al chocar con el jugador le resta vida a este
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().PlayerHealthState -= Damage;
        }
    }
    public void Disparar()
    {
        Transform A = Instantiate<GameObject>(Bala).transform;
        //A.rotation = DisparoPos.transform.rotation;
        A.position = DisparoPos.transform.position;
    }

    IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(3f);
        disparar = true;
    }
}
