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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameController.BuildState)
            {
                gameController.InfrastructureController.DestroyNewInfrastructure();
                gameController.BuildState = false;
            }
            else
            {
                
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach(var tile in gameController.BoardController.TilesList)
            {
                tile.GetComponentInChildren<TileCoordinates>().AbleCoordinates(); 
            }
        }
            

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
