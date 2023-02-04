using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] orbPrefab;

    public GameObject FactoryMethod(int tag)
    {
        GameObject orb = orbPrefab[tag];
        return orb;
    }
}
