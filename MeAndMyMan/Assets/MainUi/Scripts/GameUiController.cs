using System; 
using System.IO; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    float cameraRotationSpeed = 0.5f;
    float cameraZoomSpeed = 0.5f;

    GameController gameController;
    MouseController mouseController;
    InfrastructureController infrastructureController;
    BoardController boardController;
    Camera mainCamera;
    GameObject gameCamera;
    GameUiCameraController gameCameraController;
    GameUiMenuController gameUiMenuController;
    MenuUiSectionController menuUiSectionController;
    MenuUiTabController menuUiTabController;


    void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        gameCameraController = gameController.GameCameraController;
        gameCamera = gameCameraController.gameObject;
        mainCamera = gameCameraController.gameObject.GetComponentInChildren<Camera>();
        infrastructureController = gameController.InfrastructureController;
        gameUiMenuController = gameController.GameUiMenuController;
        menuUiSectionController = gameController.MenuUiSectionController;
        menuUiTabController = gameController.MenuUiTabController;
        boardController = gameController.BoardController;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    void Start()
    {
       


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            // if (gameUiMenuController.MenuUiState == MenuUiSectionState.infrastructureBuildState)
            // {
            //     // gameUiMenuController.MenuUiStateChange(MenuUiState.infrastructureBuildState);
            //     infrastructureController.DestroyInstantiateInfrastructure();
            // }
            
            // gameUiMenuController.MenuUiStateChange(MenuUiSectionState.infrastructureCreateState);

            // if (gameUiMenuController.MenuUiState == MenuUiState.infrastructureAboutState)
            // {
            //     gameUiMenuController.MenuUiStateChange(MenuUiState.infrastructureAboutState);
            // }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            boardController.AbleBoardCoordinates();
        }

        if (Input.GetKey(KeyCode.UpArrow)) //++ add mouse scroll wheel
        {
            if(gameCamera.transform.position.y > -5.0f)
            {
                gameCamera.transform.position += mainCamera.transform.forward * cameraZoomSpeed; 

            }

        }

        if (Input.GetKey(KeyCode.DownArrow)) //++ add mouse scroll wheel
        {
            if (gameCamera.transform.position.y < 20.0f)
            {
                gameCamera.transform.position -= mainCamera.transform.forward * cameraZoomSpeed;

            }

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameCamera.transform.position += mainCamera.transform.right;


        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameCamera.transform.position -= mainCamera.transform.right;


        }

        if (Input.GetKey(KeyCode.Space)) //++ add mouse scroll wheel
        {
            // gameController.Puase(able)

        }


    }

    void SetInfrastructureInformation()
    {
        
    }




}
