using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameUiMouseController : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 worldPosition;
    public Vector3 WorldPosition { get { return worldPosition; } }
    Ray ray;

    GameController gameController;
    InfrastructureController infrastructureController;
    GameUiMenuController gameUiMenuController; 
    void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        infrastructureController = gameController.InfrastructureController;
        gameUiMenuController = gameController.GameUiMenuController; 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        

    }
    
    void Update()
    {
        worldPosition = GetWorldPositionInt(infrastructureController.InfrastructureLayersToHit);

        if (Input.GetMouseButtonDown(0))
        {
            if (gameUiMenuController.MenuUiState == MenuUiState.UiStateBuild)
            {
                var infrastructureNew = infrastructureController.InfrastructureNew; 
                if(infrastructureController.BuildNewInfrastructure(worldPosition))
                {
                gameUiMenuController.ExitBuild(true, infrastructureNew);
                } 
            }
        }
    }

    private Vector3 GetWorldPositionInt(LayerMask layersToHit) // return List of posible position by infrastructure size
    {
        worldPosition = GetWorldPosition(layersToHit);
        Vector3 worldPositionInt = new Vector3(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), Mathf.RoundToInt(worldPosition.z));

        return worldPositionInt;
    }

    public Vector3 GetWorldPosition(LayerMask layersToHit)
    {
        mousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit)) // hitData, maxDistance, layersToHit
        {
            worldPosition = hitData.point;
        }
        return worldPosition; 
    }

    public Vector3 WorldPositionConvert(int infrastructureSize, Vector3 worldPosition)
    {
        Vector3 worldPositionConvert = worldPosition;
        float convertValue = ((infrastructureSize + 1) % 2f) / 2f;
        worldPositionConvert += new Vector3(convertValue, 0, convertValue);
        return worldPositionConvert;
    }

}
