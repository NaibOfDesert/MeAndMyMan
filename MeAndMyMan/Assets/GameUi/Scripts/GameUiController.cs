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
    GameCameraController gameCameraController;



    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        gameCameraController = gameController.GameCameraController;
        gameCamera = gameCameraController.gameObject;
        mainCamera = gameCameraController.gameObject.GetComponentInChildren<Camera>();
        infrastructureController = gameController.InfrastructureController;
        boardController = gameController.BoardController;



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameController.BuildState)
            {
                infrastructureController.DestroyNewInfrastructure();
                gameController.BuildState = false;
            }
            else
            {
                
            }
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


    }

  




}
