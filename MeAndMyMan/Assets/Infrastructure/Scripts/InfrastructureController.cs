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
    int houseAreaSize = 0;
    List<Infrastructure> houseList;
    
    [SerializeField] GameObject farmPrefab;
    public GameObject FarmPrefab { get { return farmPrefab; } }
    int farmSize = 2;
    int farmAreaSize = 1;
    List<Infrastructure> farmList;



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

    List<float> infrastructureRotationsList;



    // GameObject newInfrastructureObject; 
    
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
                infrastructureObject = new House(objectType, houseAreaSize, ObjectLevel.Level1);
                InstantiateInfrastructure(housePrefab, infrastructureObject, houseSize);

                break;
            case ObjectType.Farm:
                infrastructureObject = new Farm(objectType, farmAreaSize, ObjectLevel.Level1);
                InstantiateInfrastructure(farmPrefab, infrastructureObject, farmSize);

                break;
            default:
                Debug.Log("InitiateInfrastructure error");
                break;
        }




    }

    Infrastructure InstantiateInfrastructure(GameObject prefabObject, Object infrastructureObject, int infrastructureSize)
    {
        int infrastructureRotation = Random.Range(0, infrastructureRotationsList.Count() - 1);

        GameObject newInfrastructureObject = Instantiate(prefabObject, mouseController.WorldPosition, Quaternion.identity); // is newInfrastructure needed?? use infrastructureRotation ???
        newInfrastructure = newInfrastructureObject.GetComponent<Infrastructure>();
        newInfrastructure.InitiateInfrastructure(infrastructureObject, infrastructureSize, infrastructureRotationsList[infrastructureRotation]);

        gameController.BuildState = true;
        boardController.AbleBoardPlane(); 

        return newInfrastructure; 
    }

    public void BuildInfrastructure(Vector3 worldPosition)
    {
        // newInfrastructure.BoardList = boardController.BoardCheck(worldPosition, newInfrastructure.InfrastructureSize);
        // newInfrastructure.BoardAreaList = boardController.BoardAreaCheck(mouseController.WorldPosition, newInfrastructure.InfrastructureSize, newInfrastructure.InfrastructureObject.AreaSize);

        if (!newInfrastructure.BoardList.Any(n => n.IsPlaceable == false) && newInfrastructure.BoardList.Count() == Mathf.Pow(newInfrastructure.InfrastructureSize, 2)) /// implemented as square objects
        {

            boardController.BoardAreaSetAsUsedByInfrastructure(newInfrastructure.BoardList, newInfrastructure);
            boardController.BoardAreaSetAsUsedByInfrastructureArea(newInfrastructure.BoardAreaList, newInfrastructure); 
            boardController.AbleBoardPlane();
            boardController.BoardAreaClear(newInfrastructure.BoardAreaList, newInfrastructure.BoardAreaList);

            newInfrastructure.IsPlaced = true;
            newInfrastructure.SetDefaultMaterial();
            newInfrastructure.TextAreaValueAble(); 
            newInfrastructure = null;
            AddNewInfrastructureToList();

            gameController.BuildState = false;

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
        List<Tile> boardAreaCheckList = boardController.BoardAreaCheck(mouseController.WorldPosition, newInfrastructure.InfrastructureSize, newInfrastructure.InfrastructureObject.AreaSize);

        boardController.BoardAreaClear(boardAreaCheckList, newInfrastructure.BoardAreaList);

        Destroy(newInfrastructure.gameObject);
        newInfrastructure = null;
        gameController.BuildState = false;
    }

    public void DestroyInfrastructure()
    {

    }



}

