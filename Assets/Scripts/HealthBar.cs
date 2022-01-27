using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Cuando la salud del objeto que lleva este script se actualiza llama a esta funcion
    //para que se actualice visualmente
    public Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
