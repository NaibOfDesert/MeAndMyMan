using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    Field field;
    public Field Field { get { return field; } }

    TilePlane tilePlane; 
    public TilePlane TilePlane { get { return tilePlane; } }

    [SerializeField] bool isUsedByInfrastructure; // to rename isUsedByInfrastructure
    public bool IsUsedByInfrastructure { get { return isUsedByInfrastructure; } set { isUsedByInfrastructure = value; } }

    [SerializeField] Infrastructure usedByInfrastructure;
    public Infrastructure UsedByInfrastructure { get { return usedByInfrastructure; } set { usedByInfrastructure = value; } }

    [SerializeField] bool isUsedByInfrastructureArea;
    public bool IsUsedByInfrastructureArea { get { return isUsedByInfrastructureArea; } set { isUsedByInfrastructureArea = value; } }

    [SerializeField] FieldType fieldType;





    void Awake()
    {
        field = new Field(fieldType);
        tilePlane = GetComponentInChildren<TilePlane>();

        isUsedByInfrastructure = false;
        usedByInfrastructure = null;

        isUsedByInfrastructureArea = false; 

       
    }

    void Update()
    {
        
    }

    public void AbleMeshRenderer()
    {
        GetComponentInChildren<MeshRenderer>().enabled = !GetComponentInChildren<MeshRenderer>().enabled;
    }

    public void SetUsedByDefault()
    {
        Debug.Log("SetUsedByDefault");
        isUsedByInfrastructure = false;
        usedByInfrastructure = null;

        isUsedByInfrastructureArea = false;

    }

}

