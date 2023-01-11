using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false; // --
    public bool IsPlaceable { get { return isPlaceable; } set { isPlaceable = value; } }
    void Awake()
    {   
        
    }

    void Update()
    {
        
    }

}

