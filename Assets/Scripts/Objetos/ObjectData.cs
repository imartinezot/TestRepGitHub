using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object Data", order = 51)]
public class ObjectData : ScriptableObject
{
    public enum TIPO { Key, Coin, HealthPotion, StaminaPotion, Door}

    [SerializeField]
    string _objectName;
    [SerializeField]
    Texture _icon;
    [SerializeField]
    TIPO _objectType;
    [Header("Numero (Door/Key)")]
    [SerializeField]
    int _numero;



    public string Name { get { return _objectName; } }

    public TIPO Tipo { get { return _objectType; } }

    public Texture Icon { get { return _icon; } }

    public int Numero { get { return _numero; } }

}
