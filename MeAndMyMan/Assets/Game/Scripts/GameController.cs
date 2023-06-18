using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

/**
* Normal info
* * Important information
* ! Deprecated method, don't use
* ? Should this method be exposed in the Public API?
* TODO: refactor this method so that it coforms to the API
* @param myParam The parameter for this merhod
**/

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

    GameUiCameraController gameCameraController;
    public GameUiCameraController GameCameraController { get { return gameCameraController; } }

    GameObject gameCamera;
    public GameObject GameCamera { get { return gameCamera; } }

    GameUiMouseController mouseController;
    public GameUiMouseController MouseController { get { return mouseController; } }
    
    GameUiController gameUiController;
    public GameUiController GameUiController { get { return gameUiController; } }

    GameUiMenuController gameUiMenuController;
    public GameUiMenuController GameUiMenuController { get { return gameUiMenuController; } }

    MenuUiSectionController menuUiSectionController;
    public MenuUiSectionController MenuUiSectionController { get { return menuUiSectionController; } }

    MenuUiTabController menuUiTabController;
    public MenuUiTabController MenuUiTabController { get { return menuUiTabController; } }


    void Awake()
    {
        try
        {
        gameTimeController = FindObjectOfType<GameTimeController>();
        mouseController = FindObjectOfType<GameUiMouseController>();
        gameCameraController = FindObjectOfType<GameUiCameraController>();
        gameCamera = gameCameraController.gameObject;
        gameUiController = FindObjectOfType<GameUiController>();
        gameUiMenuController = FindObjectOfType<GameUiMenuController>();
        menuUiSectionController = FindObjectOfType<MenuUiSectionController>();
        menuUiTabController = FindObjectOfType<MenuUiTabController>();
        boardController = FindObjectOfType<BoardController>();
        infrastructureController = FindObjectOfType<InfrastructureController>();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            // *: Debug.Log("GameController: Awake basic values launched correctly"); 
        }

        gameManager = new GameManager(this, infrastructureController, BoardController);
        
    }

    void Start()
    {




    }


}
