using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] float gameLoopsCount = 0; 

    GameController gameController;
    GameManager gameManager;
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager;

        StopAllCoroutines();
        StartCoroutine(GameClock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameClock()
    {
        gameLoopsCount++;
        // gameManager.CountExpValue(); // to fix

        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(GameClock());
    }



}
    