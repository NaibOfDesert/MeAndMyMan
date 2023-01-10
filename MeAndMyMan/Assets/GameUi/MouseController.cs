using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 worldPosition;
    Ray ray;

    GameController gameController; 
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }


    private void Update()
    {
        if (gameController.IsBuildActive)
        {
                            
            if (Input.GetMouseButtonDown(0))
            {
                gameController.NewInfrastructure.IsPlaced = true;
                Debug.Log(gameController.NewInfrastructure.IsPlaced);
                gameController.NewInfrastructure = null;
                gameController.IsBuildActive = false;
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

    public Vector3 GetWorldPositionInt(LayerMask layersToHit)
    {
        worldPosition = GetWorldPosition(layersToHit); 

        Vector3 worldPositionInt = new Vector3(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), Mathf.RoundToInt(worldPosition.z)); 

        return worldPositionInt; 
    }

}
