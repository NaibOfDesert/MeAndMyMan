using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    Field field;
    public Field Field { get { return field; } }

    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable { get { return isPlaceable; } set { isPlaceable = value; } }

    [SerializeField] Infrastructure usedByInfrastructure;
    public Infrastructure UsedByInfrastructure { get { return usedByInfrastructure; } set { usedByInfrastructure = value; } }

    [SerializeField] bool usedByInfrastructureArea;
    public bool UsedByInfrastructureArea { get { return usedByInfrastructureArea; } set { usedByInfrastructureArea = value; } }

    [SerializeField] FieldType fieldType;





    void Awake()
    {
        field = new Field(fieldType);
        usedByInfrastructure = null;
        usedByInfrastructureArea = false; 
    }

    void Update()
    {
        
    }

    public void AbleMeshRenderer()
    {
        GetComponentInChildren<MeshRenderer>().enabled = !GetComponentInChildren<MeshRenderer>().enabled;
    }

}

