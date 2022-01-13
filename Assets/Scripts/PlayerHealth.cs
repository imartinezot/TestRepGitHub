using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int PlayerHealthState = 100;
    public GameObject Muerto;
    public HealthBar healthBar;
    public float tiempo = 0f;
    public float tiempoMuerto = 3f;
    public GameObject Ganado;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetHealth(PlayerHealthState);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(PlayerHealthState);
        if (PlayerHealthState <=0)
        {
            tiempo = tiempo + Time.deltaTime;
            Die();
            if (tiempo >=tiempoMuerto)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    public void Die()
    {
        //Time.timeScale = 0;
        Muerto.SetActive(true);
        GameObject.Find("Camera").GetComponent<MoveCamera>().cameraon = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
    }

    public void Exito()
    {
        Ganado.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finish")
        {
            Exito();
        }
    }
}
