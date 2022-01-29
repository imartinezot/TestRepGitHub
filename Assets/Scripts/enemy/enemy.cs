using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 50; //salud del enemigo
    public int Damage = 30; // daño que inflinge al player
    public HealthBar enemyHealthBar; //imagen de la salud restante

    //Disparo
    public GameObject DisparoPos; //desde donde sale la bala
    public GameObject Bala; //Prefab de la bala

    //Estado del disparo
    public bool EstadoDisparar; //Cuando el enemigo entra en estado ATTACK
    public bool disparar; //if true = dispara, y se pone en false

    public void Start()
    {
        enemyHealthBar.SetHealth(health);

    }
    public void Update()
    {
        enemyHealthBar.SetHealth(health);//acutaliza barra de vida con la variable de salud del personaje

        //Si está en estado disparar dispara una vez, espera unos segundos y vuelve a disparar 
        //mientras siga viendo al jugador
        if (EstadoDisparar == true && disparar == true)
        {
            Disparar();
            disparar = false;
            StartCoroutine(WaitForShoot());
        }
    }
    //funcion llamada desde gun, le resta la vida al enemigo. Le resta segun la variable establecida en el arma
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

    //al chocar con el jugador le resta vida al jugador
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().PlayerHealthState -= Damage;
        }
    }

    //Cuando se llama instancia una bala en la punta de los dedos de la mano derecha
    public void Disparar()
    {
        //metodo antiguo de instanciar la bala

        //Transform A = Instantiate<GameObject>(Bala).transform;
        ////A.rotation = DisparoPos.transform.rotation;
        //A.position = DisparoPos.transform.position;

        //Metodo con ObjectPool
        GameObject a = Pool.singleton.Get("Bala");
        //Si el objeto que he recibido no está vacío(osea que se puede usar)
        if (a != null)
        {
            //Le pasamos una posición a ese asteroide concreto
            a.transform.position = DisparoPos.transform.position;
            //Y activamos el asteroide
            a.SetActive(true);
        }
    }

    //coroutina para retrasar el siguiente disparo de bala
    IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(3f);
        disparar = true;
    }
}
