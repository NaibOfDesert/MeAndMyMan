using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    Field field;
    public Field Field { get { return field; } }

    [SerializeField] bool isPlacable = true;
    public bool IsPlacable { get { return isPlacable; } set { isPlacable = value; } }

    [SerializeField] Infrastructure usedByInfrastructure;
    public Infrastructure UsedByInfrastructure { get { return usedByInfrastructure; } set { usedByInfrastructure = value; } }

    void Awake()
    {
        field = new Field();
    }

    void Update()
    {
        
    }

}

