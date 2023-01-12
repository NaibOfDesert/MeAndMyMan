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
            // move to GameInfrastructure
            gameController.InfrastructureController.Infrastructure = Instantiate(infrastructurePrefab, mouseController.WorldPosition, Quaternion.identity);
            gameController.IsBuildActive = true;

        }
    }
}
