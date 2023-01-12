using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int gameSize;
    public int GameSize { get { return gameSize; } }

    [SerializeField] bool isBuildActive;
    public bool IsBuildActive { get { return isBuildActive; } set { isBuildActive = value; } }


    GameManager gameManager;
    public GameManager GameManager { get { return gameManager; } }

    InfrastructureController infrastructureController;
    public InfrastructureController InfrastructureController { get { return infrastructureController; } }

    [SerializeField] BoardController boardController;
    public BoardController BoardController { get { return boardController; } }

    void Awake()
    {
        gameManager = new GameManager();
        infrastructureController = FindObjectOfType<InfrastructureController>();
        boardController = FindObjectOfType<BoardController>();




    }

    void Start()
    {

    }



}
