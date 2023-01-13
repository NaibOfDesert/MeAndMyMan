using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfrastructureController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject housePrefab;
    public GameObject HousePrefab { get { return housePrefab; } }
    int houseSize = 1;
    Infrastructure[] houseList; // list !!!
    
    [SerializeField] GameObject farmPrefab;
    public GameObject FarmPrefab { get { return farmPrefab; } }
    int farmSize = 2;
    Infrastructure[] farmList; // list !!!

    [Header("Infrastructure")]
    [SerializeField] LayerMask infrastructureLayersToHit;
    public LayerMask InfrastructureLayersToHit { get { return infrastructureLayersToHit; } }

    GameObject newInfrastructureObject; 
    
    Infrastructure newInfrastructure;
    public Infrastructure NewInfrastructure { get { return newInfrastructure; } set { newInfrastructure = value; } }

    GameController gameController;
    BoardController boardController;
    MouseController mouseController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        boardController = gameController.BoardController;
        mouseController = gameController.MouseController; 

    }
/*
    bool BuildCheckField(Vector3 worldPositon)
    {
        var fieldCheck = gameController.BoardController.TilesList.Single(n => n.gameObject.transform.position == worldPositon);
        Debug.Log(fieldCheck.gameObject.transform.position);

        return (fieldCheck.Field.IsPlacable);

    }*/

    public void CreateInfrastructure(ObjectType objectType)
    {
        Object infrastructureObject;

        switch (objectType)
        {
            
            case ObjectType.House:
                infrastructureObject = new House(objectType);
                InstantiateInfrastructure(housePrefab, infrastructureObject, houseSize);

                break;
            case ObjectType.Farm:
                infrastructureObject = new Farm(objectType);
                InstantiateInfrastructure(farmPrefab, infrastructureObject, farmSize);

                break;
            default:
                Debug.Log("InitiateInfrastructure error");
                break;
        }




    }

    Infrastructure InstantiateInfrastructure(GameObject prefabObject, Object infrastructureObject, int infrastructureSize)
    {
        newInfrastructureObject = Instantiate(prefabObject, mouseController.WorldPosition, Quaternion.identity); // is newInfrastructure needed??
        newInfrastructure = newInfrastructureObject.GetComponent<Infrastructure>();
        newInfrastructure.InitiateInfrastructure(infrastructureObject, infrastructureSize); 

        gameController.BuildState = true;

        return newInfrastructure; 
    }

    public void BuildInfrastructure(Vector3 worldPositon)
    {
        Tile tileToBuild = boardController.GetBoardTile(worldPositon); 

        if (tileToBuild.Field.IsPlacable)
        {
            tileToBuild.Field.IsPlacable = false;
            newInfrastructure.IsPlaced = true;
            newInfrastructure = null;
            gameController.BuildState = false;
        }

    }

    public bool BoardCheck()
    {


        return true; 
    }

}

