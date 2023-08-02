using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLightController : MonoBehaviour
{
    new Transform transform; 
    new Light light; 

    
    GameController gameController; 
    GameTimeController gameTimeController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameTimeController = gameController.GameTimeController; 


    }

    void Start()
    {
        transform = GetComponent<Transform>(); 
        light = GetComponentInChildren<Light>();
    }

    void Update()
    {
        
    }
}
