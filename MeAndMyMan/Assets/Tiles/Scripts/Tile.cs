using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    Field field;
    public Field Field { get { return field; } }

    void Awake()
    {
        field = new Field();
    }

    void Update()
    {
        
    }

}

