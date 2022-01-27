using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Para usar la Inteligencia Artificial

public class State
{
    //Generamos una lista de estados de ese guardia
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
    };

    //Generamos una lista de eventos que tendrán todos esos estados
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    //Para conocer el estado en el que está el guardia necesitamos una variable
    public STATE name;
    //Protected = podemos acceder a esta referencia desde este script u otros derivados de este
    //Para conocer el evento del estado actual en el que estamos
    protected EVENT stage;
    //Para referenciar al guardia sobre el que queremos aplicar la máquina de estados
    protected GameObject npc;
    //Referenciamos el componente de Animator del guardia
    protected Animator anim;
    //Para conocer la posición del jugador
    protected Transform player;
    //Referenciamos el Script para obtener el siguiente estado al que ir desde otro sitio
    protected State nextState;
    //Para conocer que objeto tiene la IA
    protected NavMeshAgent agent;

    //Variable para saber la distancia mínima en la que el guardia nos avistará
    float vistDist = 50.0f;
    //Variable para conocer el ángulo de visión máximo del guardia para vernos
    float visAngle = 60.0f;
    //Variable para conocer la distancia mínima a la que tenemos que estar del guardia para que empiece a dispararnos
    float shootDist = 7.0f;

    //Creamos un Constructor de la clase para un estado que necesitará de todas esas referencias para realizar su labor correctamente
    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        //Las referencias que pasamos al llamar al Constructor quedan guardadas en las que debemos usar para realizar un estado concreto
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    //Estos métodos sobreescribirán los eventos dentro del estado en el que estemos
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    //Método para generar el desarrollo de cada uno de los estados
    public State Process()
    {
        //Si el evento en el que estoy es el de entrada, hago el método correspondiente de entrada
        if (stage == EVENT.ENTER) Enter();
        //Si el evento en el que estoy es el de update, hago el método correspondiente
        if (stage == EVENT.UPDATE) Update();
        //Si el evento en el que estoy es el de salida, hago el método correspondiente
        if (stage == EVENT.EXIT)
        {
            Exit();
            //Y devolvemos el siguiente estado al que ir
            return nextState;
        }
        //Devolvemos el resultado del método
        return this;
    }

    //Método para conocer si el guardia ve al jugador
    public bool CanSeePlayer()
    {
        //Variable para conocer la distancia y dirección entre el guardia y el jugador
        Vector3 direction = player.position - npc.transform.position;
        //Calculamos el ángulo de visión del guardia con respecto hacia donde está mirando
        float angle = Vector3.Angle(direction, npc.transform.forward);
        //Si la distancia entre el jugador y el guardia y también el ángulo de este son menores que los estipulados para que nos vea
        if (direction.magnitude < vistDist && angle < visAngle) //magnitude = coge el vector y saca su longitud, o lo hace positivo
        {
            //El guardia puede ver al jugador
            return true;
        }
        //Si el guardia no nos puede ver
        return false;
    }

    //Método para conocer si el jugador está detrás del guardia
    public bool IsPlayerBehind()
    {
        //Variable para conocer la distancia y dirección entre el jugador y el guardia
        Vector3 direction = npc.transform.position - player.position;
        //Calculamos el ángulo de visión del jugador con respecto hacia donde está mirando
        float angle = Vector3.Angle(direction, player.transform.forward);
        //Si la distancia entre el guardia y el jugador y también el ángulo de este son menores que los estipulados para que estemos detrás (o lo tengamos delante)
        if (direction.magnitude < 2 && angle < 30) //magnitude = coge el vector y saca su longitud, o lo hace positivo
        {
            //El jugador estará detrás del guardia
            return true;
        }
        //El jugador no estará detrás del guardia
        return false;
    }

    //Método para conocer si el guardia puede disparar al jugador o no
    public bool CanAttackPlayer()
    {
        //Variable para conocer la distancia y dirección entre el guardia y el jugador
        Vector3 direction = player.position - npc.transform.position;
        //Comprobamos si se ha acercado lo suficiente para poder atacar
        if (direction.magnitude < shootDist)
        {
            //El guardia puede atacar
            return true;
        }
        //Si el jugador no está suficientemente cerca
        return false;
    }

}

/*CLASES DERIVADAS DE LA DE ARRIBA*/
//Osease estados derivados o generados a partir de la clase(script) Estado

//Creamos una clase derivada para el estado IDLE
public class Idle : State
{
    //Usamos el constructor de la clase STATE para pasar todas las referencias necesarias para la consecución correcta de este estado
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        //Pasamos el nombre del estado que queremos que se haga
        name = STATE.IDLE;
    }

    //Sobreescribimos el evento Enter de ese estado 
    public override void Enter()
    {
        //Hacemos la animación de Idle
        anim.SetTrigger("isIdle");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //Sobreescribimos el evento Update de ese estado
    public override void Update()
    {
        //Si el guardia mientras está en Idle ve al jugador
        if (CanSeePlayer())
        {
            //Le digo al guardia que pase al siguiente estado
            nextState = new Pursue(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
        //Esperamos mientras no se cumpla la condición(osea el guardia aquí no nos está viendo)
        else if (Random.Range(0, 100) < 10)
        {
            //Si la condición se ha cumplido pasamos al estado de patrulla
            nextState = new Patrol(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
    }

    //Sobreescribimos el evento Exit de ese estado
    public override void Exit()
    {
        //Para evitar errores de animación
        anim.ResetTrigger("isIdle");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

//Creamos una clase derivada para el estado PATROL
public class Patrol : State
{
    //Necesitamos un index para recorrer los puntos entre los que patrullará el guardia, si lo ponemos a -1 nos permite dejarlo como si no estuviera inicializado, porque la lista de puntos empieza en la posición 0
    int currentIndex = -1;

    //Usamos el constructor de la clase STATE para pasar todas las referencias necesarias para la consecución correcta de este estado
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        //Pasamos el nombre del estado que queremos que se haga
        name = STATE.PATROL;
        //Le ponemos una velocidad al agent de ese NPC
        agent.speed = 2;
        //Le decimos al agent que ese NPC no tiene que estar parado
        agent.isStopped = false;
    }

    //Sobreescribimos el evento Enter de ese estado 
    public override void Enter()
    {
        //Creamos una variable que luego usaremos para un cálculo de distancia
        float lastDist = Mathf.Infinity;
        //Bucle para recorrer toda la lista de checkpoints que tenemos
        for (int i = 0; i < GameEnvironment.Singleton.CheckPoints.Count; i++)
        {
            //Creamos una referencia al WayPoint al que debe ir el guardia
            GameObject thisWP = GameEnvironment.Singleton.CheckPoints[i];
            //Distancia entre el guardia el checkpoint o waypoint concreto al que queremos que vaya
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            //Si estamos suficiente cerca de un WayPoint
            if (distance < lastDist)
            {
                //El index que usamos para la patrulla
                currentIndex = i - 1;
                //Restablecemos las distancias
                lastDist = distance;
            }
        }
        //Hacemos que el guardia empiece a andar
        anim.SetTrigger("isWalking");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //Sobreescribimos el evento Update de ese estado
    public override void Update()
    {
        //Si el guardia se ha acercado lo suficiente a un Checkpoint
        if (agent.remainingDistance < 1)
        {
            //Si la posición de el array de checkpoints es mayor que el tamaño del array de checkpoints
            if (currentIndex >= GameEnvironment.Singleton.CheckPoints.Count - 1)
                //Reseteamos esa posición a 0 para comenzar otra vuelta
                currentIndex = 0;
            //Si la posición del array de checkpoints no es mayor que el tamaño del array
            else
                //Sumaremos una posición
                currentIndex++;
            //Mandaremos al guardia a hacer patrulla a ese checkpoint concreto
            agent.SetDestination(GameEnvironment.Singleton.CheckPoints[currentIndex].transform.position);
        }

        //Si el guardia puede ver al jugador
        if (CanSeePlayer())
        {
            //Le digo al guardia que pase al siguiente estado
            nextState = new Pursue(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
        //Si el jugador está justo detrás del guardia
        else if (IsPlayerBehind()) //Si se cumple este método
        {
            //Le digo al guardia que pase al siguiente estado
            nextState = new Runaway(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
    }

    //Sobreescribimos el evento Exit de ese estado
    public override void Exit()
    {
        //Para evitar errores de animación
        anim.ResetTrigger("isWalking");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

//Creamos una clase derivada para el estado PURSUE
public class Pursue : State
{
    //Usamos el constructor de la clase STATE para pasar todas las referencias necesarias para la consecución correcta de este estado
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        //Pasamos el nombre del estado que queremos que se haga
        name = STATE.PURSUE;
        //Le ponemos una velocidad al agent de ese NPC
        agent.speed = 5;
        //Le decimos al agent que ese NPC no tiene que estar parado
        agent.isStopped = false;
    }

    //Sobreescribimos el evento Enter de ese estado 
    public override void Enter()
    {
        //Hacemos que el guardia empiece a correr
        anim.SetTrigger("isRunning");
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //Sobreescribimos el evento Update de ese estado
    public override void Update()
    {
        //Mandamos al guardia hacia el jugador
        agent.SetDestination(player.position);
        //Mientras el guardia tenga un camino que seguir
        if (agent.hasPath)
        {
            //El guardia comprobará si puede pasar a atacar al jugador
            if (CanAttackPlayer())//Y si se cumple ese método
            {
                //Le digo al guardia que pase al siguiente estado
                nextState = new Attack(npc, agent, anim, player);
                //Luego iremos al Evento de Exit de este estado
                stage = EVENT.EXIT;
            }
            //Si el guardia pierde de vista al jugador
            else if (!CanSeePlayer())//Si no se cumple este método
            {
                //Le digo al guardia que pase al siguiente estado
                nextState = new Patrol(npc, agent, anim, player);
                //Luego iremos al Evento de Exit de este estado
                stage = EVENT.EXIT;
            }
        }

    }

    //Sobreescribimos el evento Exit de ese estado
    public override void Exit()
    {
        //Para evitar errores de animación
        anim.ResetTrigger("isRunning");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

//Creamos una clase derivada para el estado ATTACK
public class Attack : State
{
    //Velocidad de rotación del guardia
    float rotationSpeed = 2.0f;
    //Sonido de disparo
    AudioSource shoot;
    //Usamos el constructor de la clase STATE para pasar todas las referencias necesarias para la consecución correcta de este estado
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        //Pasamos el nombre del estado que queremos que se haga
        name = STATE.ATTACK;
        //Para obtener el sonido de disparo
        shoot = _npc.GetComponent<AudioSource>();
    }

    //Sobreescribimos el evento Enter de ese estado 
    public override void Enter()
    {
        //Hacemos que el guardia empiece a disparar
        anim.SetTrigger("isShooting");
        //Mientras dispara paramos al guardia
        agent.isStopped = true;
        //Reproducimos el sonido de disparo
        shoot.Play();
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //Sobreescribimos el evento Update de ese estado
    public override void Update()
    {
        //Variable para conocer la distancia y dirección entre el guardia y el jugador
        Vector3 direction = player.position - npc.transform.position;
        //Calculamos el ángulo de visión del guardia con respecto hacia donde está mirando
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;
        //Haremos que el guardia rote sobre sí mismo apuntando
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        //Cuando el guardia no pueda seguir atacando al jugador
        if (!CanAttackPlayer())//Si el método no se cumple
        {
            //Le digo al guardia que pase al siguiente estado (en este caso que se quede quieto)
            nextState = new Idle(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
    }

    //Sobreescribimos el evento Exit de ese estado
    public override void Exit()
    {
        //Para evitar errores de animación
        anim.ResetTrigger("isShooting");
        //Paramos el sonido de disparo
        shoot.Stop();
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}

//Creamos una clase derivada para el estado RUNAWAY
public class Runaway : State
{
    //Creamos una referencia para la zona segura
    GameObject safeLocation;
    //Usamos el constructor de la clase STATE para pasar todas las referencias necesarias para la consecución correcta de este estado
    public Runaway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        //Pasamos el nombre del estado que queremos que se haga
        name = STATE.RUNAWAY;
        safeLocation = GameObject.FindGameObjectWithTag("Safe");
    }

    //Sobreescribimos el evento Enter de ese estado 
    public override void Enter()
    {
        //Hacemos que el guardia empiece a correr
        anim.SetTrigger("isRunning");
        //Hacemos que el guardia no esté parado
        agent.isStopped = false;
        //Aumentamos la velocidad del guardia
        agent.speed = 6;
        //Mandamos al guardia a la zona segura
        agent.SetDestination(safeLocation.transform.position);
        //Llamamos al método Enter de la clase State
        base.Enter();
    }

    //Sobreescribimos el evento Update de ese estado
    public override void Update()
    {
        //Si el guardia está ya prácticamente en la zona segura
        if (agent.remainingDistance < 1)
        {
            //Le digo al guardia que pase al siguiente estado
            nextState = new Patrol(npc, agent, anim, player);
            //Luego iremos al Evento de Exit de este estado
            stage = EVENT.EXIT;
        }
    }

    //Sobreescribimos el evento Exit de ese estado
    public override void Exit()
    {
        //Para evitar errores de animación
        anim.ResetTrigger("isRunning");
        //Llamamos al método Exit de la clase State
        base.Exit();
    }
}
