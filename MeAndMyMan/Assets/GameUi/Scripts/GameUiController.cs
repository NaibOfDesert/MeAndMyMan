using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] Infrastructure infrastructurePrefab; //-- 



    GameController gameController;
    InfrastructureController infrastructureController;
    MouseController mouseController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        infrastructureController = gameController.InfrastructureController;
        mouseController = gameController.MouseController; 


    }

    public void BuildHouse()
    {
        if (!gameController.BuildState)
        {
            // move to GameInfrastructure
            infrastructureController.CreateInfrastructure(ObjectType.House);

        }
    }

    public void BuildFarm()
    {
        if (!gameController.BuildState)
        {
            // move to GameInfrastructure
            infrastructureController.CreateInfrastructure(ObjectType.Farm);

        }
    }
}
