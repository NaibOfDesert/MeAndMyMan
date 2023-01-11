using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool isBuildActive;
    public bool IsBuildActive { get { return isBuildActive; } set { isBuildActive = value; } }


    GameInfrastructure gameInfrastructure;
    public GameInfrastructure GameInfrastructure { get { return gameInfrastructure; } }

    GameManager gameManager;
    public GameManager GameManager { get { return gameManager; } } // is it needed? 

    void Awake()
    {
        gameInfrastructure = GetComponent<GameInfrastructure>(); 
        gameManager = new GameManager(); 
    }

    void CreateFieldList()
    {
        // foreach in object
    }
}
