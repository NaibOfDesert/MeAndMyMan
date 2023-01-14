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
            transform.position = mouseController.WorldPositionConvert(infrastructureSize);
            gameController.BoardController.BoardAreaCheck(mouseController.WorldPosition, infrastructureSize, infrastructureObject.AreaSize);
            

            // red if !IsPlacable


        }
    }

    public void InitiateInfrastructure(Object infrastructureObject, int infrastructureSize)
    {
        this.infrastructureObject = infrastructureObject;
        this.infrastructureSize = infrastructureSize;
        Debug.Log(infrastructureObject.AreaSize + " infrastructureObject.AreaSize"); 


    }



}
