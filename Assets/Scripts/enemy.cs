
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 50;
    public int Damage = 30;
    public HealthBar enemyHealthBar;

    public void Start()
    {
        enemyHealthBar.SetHealth(health);
    }
    public void Update()
    {
        enemyHealthBar.SetHealth(health);
    }
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().PlayerHealthState -= Damage;
        }
    }
}
