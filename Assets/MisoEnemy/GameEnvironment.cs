using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment
{
    //Una referencia del tipo GameEnvironment
    private static GameEnvironment sharedInstance;

    //Generamos una lista para guardar los checkpoints
    private List<GameObject> checkPoints = new List<GameObject>();
    //El accesor a esa lista
    public List<GameObject> CheckPoints { get { return checkPoints; } }

    //Creamos el Singleton
    public static GameEnvironment Singleton
    {
        get
        {
            //Si esa instancia estuviera vacía
            if (sharedInstance == null)
            {
                //En esta instancia generamos un GameEnvironment que será el que pasaremos
                sharedInstance = new GameEnvironment();
                //Pasamos la lista de checkpoints a través del Singleton, añadiendo los que hayan en la escena a nuestra lista
                sharedInstance.CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                //Pasamos los checkpoints ordenados
                //sharedInstance.checkPoints = sharedInstance.checkPoints.OrderBy(waypoint => waypoint.name).ToList();
            }
            //Este accesor nos devuelve esta instancia con su debida información
            return sharedInstance;
        }
    }



}
