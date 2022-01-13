using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object Data", order = 51)]
public class ObjectInfo : ScriptableObject
{
    public enum TIPO { Key, Coin, HealthPotion, StaminaPotion}

    [SerializeField]
    string _objectName;
    [SerializeField]
    TIPO _objectType;
    [SerializeField]
    Texture _icon;

    //Constructors

    public string Name { get { return _objectName; } }

    public TIPO Tipo { get { return _objectType; } }

    public Texture Icon { get { return _icon; } }

}
