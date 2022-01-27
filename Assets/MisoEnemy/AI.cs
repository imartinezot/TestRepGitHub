using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Para poder usar la Inteligencia Artificial

public class AI : MonoBehaviour
{

    //El agent del enemigo
    NavMeshAgent agent;
    //El animator del enemigo
    Animator anim;
    //La posición del jugador sobre la que queremos que actúe
    public Transform player;
    //El estado actual en el que está ese enemigo
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos las referencias y variables
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        //Elegimos el estado en el que empieza este enemigo
        currentState = new Idle(this.gameObject, agent, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        //Llamamos al método de la clase State que me permite que se ejecute ese estado
        currentState = currentState.Process();
    }
}
