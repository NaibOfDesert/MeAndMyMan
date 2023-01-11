using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrastructure : MonoBehaviour
{
    [SerializeField] bool isPlaced;
    public bool IsPlaced { get { return isPlaced; }  set { isPlaced = value; } }



    Object infrastructureObject; 

    GameController gameController; 
    MouseController mouseController;
    InfrastructureArea infrastructureArea;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = FindObjectOfType<MouseController>();
        infrastructureArea = FindObjectOfType<InfrastructureArea>();

        // infrastructureObject = new House(); // implement depends of type of new Infrastructure

    }

    void Start()
    {

    }

    void Update()
    {
        if (!isPlaced)
        {
            transform.position = mouseController.WorldPosition;
        }   
    }

}
