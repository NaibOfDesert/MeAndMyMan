using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO:

// NOTE: 
public class GameController : MonoBehaviour
{
    [SerializeField] int gameSize;
    public int GameSize { get { return gameSize; } }

    GameManager gameManager;
    public GameManager GameManager { get { return gameManager; } }

    GameTimeController gameTimeController;
    public GameTimeController GameTimeController { get { return gameTimeController; } }

    InfrastructureController infrastructureController;
    public InfrastructureController InfrastructureController { get { return infrastructureController; } }

    BoardController boardController;
    public BoardController BoardController { get { return boardController; } }

    GameCameraController gameCameraController;
    public GameCameraController GameCameraController { get { return gameCameraController; } }

    GameObject gameCamera;
    public GameObject GameCamera { get { return gameCamera; } }

    MouseController mouseController;
    public MouseController MouseController { get { return mouseController; } }
    
    GameUiController gameUiController;
    public GameUiController GameUiController { get { return gameUiController; } }

    GameUiMenuController gameUiMenuController;
    public GameUiMenuController GameUiMenuController { get { return gameUiMenuController; } }

    [SerializeField] MenuUiSectionController menuUiSectionController;
    public MenuUiSectionController MenuUiSectionController { get { return menuUiSectionController; } }

    MenuUiTabController menuUiTabController;
    public MenuUiTabController MenuUiTabController { get { return menuUiTabController; } }


    void Awake()
    {
        gameTimeController = FindObjectOfType<GameTimeController>();
        mouseController = FindObjectOfType<MouseController>();
        gameCameraController = FindObjectOfType<GameCameraController>();
        gameCamera = gameCameraController.gameObject;
        gameUiController = FindObjectOfType<GameUiController>();
        gameUiMenuController = FindObjectOfType<GameUiMenuController>();
        menuUiSectionController = FindObjectOfType<MenuUiSectionController>();
        menuUiTabController = FindObjectOfType<MenuUiTabController>();
        boardController = FindObjectOfType<BoardController>();
        infrastructureController = FindObjectOfType<InfrastructureController>();
        gameManager = new GameManager(this, infrastructureController, BoardController);
        gameManager.SetCosts();
    }

    void Start()
    {




    }


}
