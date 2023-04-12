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

    public void SetInfrastructure()
    {
        gameUiMenuController.MenuUiStateChange(MenuUiSectionState.infrastructureCreateState);
    }

    private void OnMouseDown()
    {
        if (infrastructure.IsPlaced)
        {
            gameUiMenuController.MenuUiStateChange(MenuUiSectionState.infrastructureInformationState, infrastructure);
        }
    }
}
