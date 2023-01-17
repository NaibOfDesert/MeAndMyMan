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

    MouseController mouseController;
    public MouseController MouseController { get { return mouseController; } }

    void Awake()
    {
        gameManager = new GameManager();
        infrastructureController = FindObjectOfType<InfrastructureController>();
        boardController = FindObjectOfType<BoardController>();
        mouseController = FindObjectOfType<MouseController>();
        gameCameraController = FindObjectOfType<GameCameraController>();



    }

    void Start()
    {

    }



}
