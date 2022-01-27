
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 50; //salud del enemigo
    public int Damage = 30; // daño que inflinge al player
    public HealthBar enemyHealthBar;

    public void Start()
    {
        enemyHealthBar.SetHealth(health);
    }
    public void Update()
    {
        enemyHealthBar.SetHealth(health);
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
}
