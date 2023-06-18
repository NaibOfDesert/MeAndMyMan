using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfrastructureUiController : MonoBehaviour
{
    Infrastructure infrastructure;
    GameController gameController;
    GameUiMenuController gameUiMenuController;

    void Awake()
    {
        infrastructure = GetComponent<Infrastructure>();
        gameController = FindObjectOfType<GameController>();
        gameUiMenuController = gameController.GameUiMenuController;
    }

    public void SetInfrastructure() // ?: to remove? 
    {

    }

    private void OnMouseDown()
    {
        if (infrastructure.IsPlaced)
        {
            gameUiMenuController.About(infrastructure);
        }
    }
}
