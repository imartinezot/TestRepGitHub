using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int PlayerHealthState = 100; //salud del personaje
    public GameObject Muerto; //UI, mensaje de game over
    public HealthBar healthBar; //barra de salud visual
    //variables para cuenta atras, una vez muerto espera 3segundos y vuelve a cargar la escena para reiniciar el bucle de juego
    public float tiempo = 0f;
    public float tiempoMuerto = 3f;

    public GameObject Ganado;//al salir del laberinto aparece un texto diciendo que has ganado
    void Start()
    {
        healthBar.SetHealth(PlayerHealthState);
    }

    //al morir espera un poco y carga la escena de nuevo
    void Update()
    {
        healthBar.SetHealth(PlayerHealthState); //le dice cuanta vida tiene el personaje a la interfaz
        if (PlayerHealthState <=0)
        {
            tiempo = tiempo + Time.deltaTime;
            Die();
            if (tiempo >=tiempoMuerto)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (PlayerHealthState > 100)
        {
            PlayerHealthState = 100;
        }
    }
    //desactiva camara, desactiva movimiento, mensaje de "has muerto"
    public void Die()
    {
        //Time.timeScale = 0;
        Muerto.SetActive(true);
        GameObject.Find("Camera").GetComponent<MoveCamera>().cameraon = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
    }

    //mensaje de victoria
    public void Exito()
    {
        Ganado.SetActive(true);
    }

    //cuando se sale del laberinto llama a la funcion para mostrar el mensaje de victoria
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finish")
        {
            Exito();
        }
    }
}
