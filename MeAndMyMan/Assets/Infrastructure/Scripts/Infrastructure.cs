using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrastructure : MonoBehaviour
{
    [SerializeField] bool isPlaced;
    public bool IsPlaced { get { return isPlaced; }  set { isPlaced = value; } }

    GameController gameController; 
    MouseController mouseController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = FindObjectOfType<MouseController>();

    }

    void Start()
    {

    }

    void Update()
    {
        if (!isPlaced)
        {
            transform.position = mouseController.GetWorldPositionInt(gameController.InfrastructureLayersToHit);
        }   
    }

}
