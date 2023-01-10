using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] Infrastructure infrastructurePrefab;

    GameController gameController;
    MouseController mouseController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = FindObjectOfType<MouseController>();
    }

    public void BuildHouse()
    {
        if (!gameController.IsBuildActive)
        {

            gameController.NewInfrastructure = Instantiate(infrastructurePrefab, mouseController.GetWorldPositionInt(gameController.InfrastructureLayersToHit), Quaternion.identity); ;
            gameController.IsBuildActive = true;

        }
    }
}
