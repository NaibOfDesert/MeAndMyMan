using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO:

// NOTE: 
public class GameController : MonoBehaviour
{
    [SerializeField] int gameSize;
    public int GameSize { get { return gameSize; } }

    [SerializeField] bool buildState;
    public bool BuildState { get { return buildState; } set { buildState = value; } } //-- ??

    [SerializeField] bool infrastructureState; // to check
    public bool InfrastructureState { get { return infrastructureState; } set { infrastructureState = value; } }

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

    GameUiMenuController gameUiMenuController;
    public GameUiMenuController GameUiMenuController { get { return gameUiMenuController; } }



    void Awake()
    {
        gameManager = new GameManager(this);
        gameManager.SetCosts(); 
        gameTimeController = FindObjectOfType<GameTimeController>();
        mouseController = FindObjectOfType<MouseController>();
        gameCameraController = FindObjectOfType<GameCameraController>();
        gameCamera = gameCameraController.gameObject;
        gameUiMenuController = FindObjectOfType<GameUiMenuController>();
        boardController = FindObjectOfType<BoardController>();
        infrastructureController = FindObjectOfType<InfrastructureController>();

        InfrastructureStateAble(); ///  to comment in final version
    }

    void Start()
    {
        buildState = false;
        infrastructureState = false;
    }

    public void BuildStateAble()
    {
        buildState = !buildState;
        if (InfrastructureState)
        {
            gameUiMenuController.MenuInfrastructureAble(null); // set all other states false?
        }

    }

    public void InfrastructureStateAble()
    {
        Debug.Log("infrastructure state change");

        infrastructureState = !infrastructureState;
    }

}
