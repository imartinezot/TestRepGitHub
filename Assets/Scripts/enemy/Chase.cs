using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    //recorrido 1 y 2 del enemigo;
    public GameObject enemigo;
    public bool MoveraPlayer = false;
    public bool enemyAI = false;
    public bool Path = true;

    public Transform Player;
    public float speed = 2;
    public float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI == true)
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().Resume();
        }
        else
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
        }

        //if (MoveraPlayer == true)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

        //    //rotación
        //    var lookPos = Player.position - transform.position;
        //    lookPos.y = 0;
        //    var rotation = Quaternion.LookRotation(lookPos);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);

        //    enemyAI = true;
        //}
    }
}
