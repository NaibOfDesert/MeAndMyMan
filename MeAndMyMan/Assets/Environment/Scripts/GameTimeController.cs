using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] float gameLoopsCount = 0;
    float gameLootTime = 10f;

    GameController gameController;
    GameManager gameManager;
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager;
        StopAllCoroutines();
    }

    void Start()
    {
        StartCoroutine(GameClock());
    }

    void Update()
    {


    }

    //TODO: clock work in diferent part of time
    IEnumerator GameClock()
    {
        gameLoopsCount++;

        yield return new WaitForSecondsRealtime(gameLootTime);
        gameManager.CalculateInfrastructureIncom(gameController.InfrastructureController);

        StartCoroutine(GameClock());
    }

    public void PauseGameClock()
    {

    }

}
    