using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrastructure : MonoBehaviour
{
    [SerializeField] bool isPlaced;
    int infrastructureSize = 0; 
    public bool IsPlaced { get { return isPlaced; }  set { isPlaced = value; } }


    InfrastructureArea infrastructureArea;

    Object infrastructureObject;  //--

    GameController gameController; 
    MouseController mouseController;
    InfrastructureController infrastructureController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController; 

        infrastructureArea = FindObjectOfType<InfrastructureArea>(); //--



        // infrastructureObject = new House(); // implement depends of type of new Infrastructure

    }

    void Start()
    {

    }

    void Update()
    {
        if (!isPlaced)
        {
            transform.position = WorldPositionConvert();
            infrastructureController.BoardCheck(); 

            // red if !IsPlacable


        }   
    }

    public void InitiateInfrastructure(Object infrastructureObject, int infrastructureSize)
    {
        this.infrastructureObject = infrastructureObject;
        this.infrastructureSize = infrastructureSize;
        Debug.Log(infrastructureObject.objectType); 


    }

    Vector3 WorldPositionConvert()
    {
        Vector3 worldPositionConvert = mouseController.WorldPosition;
        float convertValue = ((infrastructureSize + 1) % 2f) / 2f;
        worldPositionConvert += new Vector3(convertValue, 0 , convertValue);
        Debug.Log(convertValue); 
        return worldPositionConvert; 
    }

}
