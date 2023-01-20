using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiMenuController : MonoBehaviour
{
    [SerializeField] GameObject menuBuildObject;

    GameController gameController;
    MouseController mouseController;
    InfrastructureController infrastructureController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController;

    }

    void Update()
    {
        
    }

    public void BuildHouse()
    {
        if (!gameController.BuildState)
        {
            infrastructureController.CreateInfrastructure(ObjectType.House);

        }
    }

    public void BuildFarm()
    {
        if (!gameController.BuildState)
        {
            infrastructureController.CreateInfrastructure(ObjectType.Farm);

        }
    }

    public void MenuBuildStateAble()
    {
        
    }
}
