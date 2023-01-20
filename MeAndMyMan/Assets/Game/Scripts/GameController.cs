using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int gameSize;
    public int GameSize { get { return gameSize; } }

    [SerializeField] bool buildState;
    public bool BuildState { get { return buildState; } set { buildState = value; } }

    GameManager gameManager;
    public GameManager GameManager { get { return gameManager; } }

    InfrastructureController infrastructureController;
    public InfrastructureController InfrastructureController { get { return infrastructureController; } }

    [SerializeField] BoardController boardController;
    public BoardController BoardController { get { return boardController; } }

    GameCameraController gameCameraController;

    public GameCameraController GameCameraController { get { return gameCameraController; } }

    GameObject gameCamera;
    public GameObject GameCamera { get { return gameCamera; } }

    MouseController mouseController;
    public MouseController MouseController { get { return mouseController; } }

    GameUiMenuController gameUiMenuController;
    public GameUiMenuController GameUiMenuController { get { return gameUiMenuController; } }



    void Awake()
    {
        gameManager = new GameManager(this);
        infrastructureController = FindObjectOfType<InfrastructureController>();
        boardController = FindObjectOfType<BoardController>();
        mouseController = FindObjectOfType<MouseController>();
        gameCameraController = FindObjectOfType<GameCameraController>();
        gameUiMenuController = FindObjectOfType<GameUiMenuController>();

        gameCamera = gameCameraController.gameObject; 
    }

    void Start()
    {

    }

    public void BuildStateAble()
    {
        buildState = !buildState;
        gameUiMenuController.MenuBuildStateAble();
    }

}
