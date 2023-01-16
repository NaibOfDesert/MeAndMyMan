﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfrastructureController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject housePrefab;
    public GameObject HousePrefab { get { return housePrefab; } }
    int houseSize = 1;
    int houseAreaSize = 0;
    List<Infrastructure> houseList;
    
    [SerializeField] GameObject farmPrefab;
    public GameObject FarmPrefab { get { return farmPrefab; } }
    int farmSize = 2;
    int farmAreaSize = 1;
    List<Infrastructure> farmList;

    List<float> infrastructureRotationsList;


    [Header("Infrastructure")]
    [SerializeField] LayerMask infrastructureLayersToHit;
    public LayerMask InfrastructureLayersToHit { get { return infrastructureLayersToHit; } }

    [Header("Infrastructure")]
    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } }

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } }

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } }


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

        houseList = new List<Infrastructure>();
        farmList = new List<Infrastructure>();
        infrastructureRotationsList = new List<float>() { 0f, 90f, 180f, 270f}; 
    }

    public void CreateInfrastructure(ObjectType objectType)
    {
        Object infrastructureObject;


        switch (objectType)
        {
            
            case ObjectType.House:
                infrastructureObject = new House(objectType, houseAreaSize);
                InstantiateInfrastructure(housePrefab, infrastructureObject, houseSize);

                break;
            case ObjectType.Farm:
                infrastructureObject = new Farm(objectType, farmAreaSize);
                InstantiateInfrastructure(farmPrefab, infrastructureObject, farmSize);

                break;
            default:
                Debug.Log("InitiateInfrastructure error");
                break;
        }




    }

    Infrastructure InstantiateInfrastructure(GameObject prefabObject, Object infrastructureObject, int infrastructureSize)
    {
        int infrastructureRotation = Random.Range(0, infrastructureRotationsList.Count());

        newInfrastructureObject = Instantiate(prefabObject, mouseController.WorldPosition, Quaternion.identity); // is newInfrastructure needed??
        newInfrastructure = newInfrastructureObject.GetComponent<Infrastructure>();
        newInfrastructure.InitiateInfrastructure(infrastructureObject, infrastructureSize, infrastructureRotationsList[infrastructureRotation]); 

        gameController.BuildState = true;

        return newInfrastructure; 
    }

    public void BuildInfrastructure(Vector3 worldPosition)
    {
        List<Tile> boardCheckList = gameController.BoardController.BoardCheck(worldPosition, newInfrastructure.InfrastructureSize);

        if (!boardCheckList.Any(n => n.IsPlacable == false) && boardCheckList.Count() == Mathf.Pow(newInfrastructure.InfrastructureSize, 2)) /// implemented as square objects
        {
            foreach (var tile in boardCheckList)
            {
                tile.UsedByInfrastructure = newInfrastructure; 
                tile.IsPlacable = false;
            }

            newInfrastructure.IsPlaced = true;
            newInfrastructure.SetDefaultMaterial();
            newInfrastructure = null;
            AddNewInfrastructureToList();

            gameController.BuildState = false;
            gameController.BoardController.BoardAreaClear();
        }
    }

    void AddNewInfrastructureToList()
    {
        if(newInfrastructure != null)
        {
            switch (newInfrastructure.InfrastructureObject.ObjectType)
            {
                case ObjectType.House:
                    houseList.Add(newInfrastructure); 
                    break;

                case ObjectType.Farm:
                    farmList.Add(newInfrastructure);
                    break;

                default:
                    Debug.Log("AddNewInfrastructureToList error");
                    break;
            }
        }
    }

    public void DestroyNewInfrastructure()
    {
        gameController.BoardController.BoardAreaClear();

        Destroy(newInfrastructure.gameObject);
        newInfrastructure = null;
        gameController.BuildState = false;
    }

    public void DestroyInfrastructure()
    {

    }



}

