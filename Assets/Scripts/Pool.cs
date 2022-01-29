using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creamos una clase que almacene las propiedades de cada Item que meteremos en el Pool
[System.Serializable]
public class PoolItem
{
    //Prefabricado de este Item concreto
    public GameObject prefab;
    //Cantidad de este Item
    public int amount;
    //Variable para hacer expandible este pool
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    //Creamos un Singleton de esta clase para que pueda ser usado desde otras
    public static Pool singleton;
    //Creamos una lista de Items que serán usados en el Pool
    public List<PoolItem> items;
    //Creamos otra lista de objetos que se han usado en el Pool, o que se usan
    public List<GameObject> pooledItems;

    private void Awake()
    {
        //Compruebo que la referencia Singleton está vacía
        if (singleton == null)
        {
            //Rellenamos el Singleton
            singleton = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //La lista de pooledItems tenemos que rellenarla
        pooledItems = new List<GameObject>();
        //Hacemos una pasada por la lista de PoolItems, y guardamos en esta nueva lista, la cantidad de objetos que hayan de ese tipo
        foreach (PoolItem item in items)
        {
            //Guardamos la cantidad de objetos que hay de cada tipo
            for (int i = 0; i < item.amount; i++)
            {
                //Creamos una referencia para guardar GameObjects en la lista y también los instanciamos en la escena
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                //Los añadimos a la lista
                pooledItems.Add(obj);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Método que nos devolverá un GameObject asociado a una tag
    public GameObject Get(string tag)
    {
        //Hacemos una pasada por toda la lista de objetos creados mediante el Pool
        for (int i = 0; i < pooledItems.Count; i++)
        {
            //Si el objeto que vemos no está activo en la jerarquía y su tag coincide con la que le hemos pasado
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                //Nos devuelve ese objeto en concreto de la lista
                return pooledItems[i];
            }
        }
        //Hacemos una pasada por la lista de PoolItems, y guardamos en esta nueva lista, el nuevo objeto que ha sido creado de un tipo elegido, ya que hemos puesto que en este caso será expandible
        foreach (PoolItem item in items)
        {
            //Si el objeto que encontramos en esa lista tiene el mismo tag que buscamos y además es expandible
            if (item.prefab.tag == tag && item.expandable)
            {
                //Generamos una referencia al gameObject
                GameObject obj = Instantiate(item.prefab);
                //Y lo desactivamos de primeras
                obj.SetActive(false);
                //Lo añadimos a la lista de objetos del pool
                pooledItems.Add(obj);
                //Nos devuelve ese objeto en concreto de la lista
                return obj;
            }
        }
        //Me devuelve un objeto vacío
        return null;
    }
}
