using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 worldPosition;
    public Vector3 WorldPosition { get { return worldPosition; } }
    Ray ray;

    GameController gameController; 
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }


    void Update()
    {
        worldPosition = GetWorldPositionInt(gameController.InfrastructureController.InfrastructureLayersToHit);

        if (gameController.IsBuildActive) // as second?
        {
                            
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(worldPosition);
                // Linq to find element from 
                gameController.InfrastructureController.BuildInfrastructure(worldPosition);

                

            }
        }

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

    private Vector3 GetWorldPositionInt(LayerMask layersToHit)
    {
        worldPosition = GetWorldPosition(layersToHit); 

        Vector3 worldPositionInt = new Vector3(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), Mathf.RoundToInt(worldPosition.z)); 

        return worldPositionInt; 
    }

}
