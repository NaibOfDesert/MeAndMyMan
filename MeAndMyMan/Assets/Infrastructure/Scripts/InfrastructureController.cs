using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfrastructureController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject housePrefab;
    public GameObject HousePrefab { get { return housePrefab; } }

    List<Infrastructure> houseList;
    public List<Infrastructure> HouseList { get { return houseList; } }

    [SerializeField] GameObject farmPrefab;
    public GameObject FarmPrefab { get { return farmPrefab; } }

    [SerializeField] List<Infrastructure> farmList;
    public List<Infrastructure> FarmList { get { return farmList; } }


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
    
    Infrastructure newInfrastructure;
    public Infrastructure NewInfrastructure { get { return newInfrastructure; } set { newInfrastructure = value; } }

    GameController gameController;
    GameManager gameManager;
    MouseController mouseController;
    GameUiMenuController gameUiMenuController;
    BoardController boardController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager;
        mouseController = gameController.MouseController;
        gameUiMenuController = gameController.GameUiMenuController;
        boardController = gameController.BoardController;

        houseList = new List<Infrastructure>();
        farmList = new List<Infrastructure>();
        infrastructureRotationsList = new List<float>() { 0f, 90f, 180f, 270f}; 
    }

    public void CreateInfrastructure(ObjectType objectType)
    {
        if(!gameManager.CalculateBuildInfrastructure(objectType))
        {
            // throw new System.ArgumentOutOfRangeException();
            return; 
        }
        /*try
        {
        }
        catch
        {

        }*/

        Object infrastructureObject;
        gameController.BuildStateAble();

        switch (objectType)
        {
            
            case ObjectType.House:
                infrastructureObject = new House();
                InstantiateInfrastructure(housePrefab, infrastructureObject, infrastructureObject.Size);

                break;
            case ObjectType.Farm:
                infrastructureObject = new Farm();
                InstantiateInfrastructure(farmPrefab, infrastructureObject, infrastructureObject.Size);

                break;
            default:
                Debug.Log("CreateInfrastructure error");
                break;
        }
    }

    Infrastructure InstantiateInfrastructure(GameObject prefabObject, Object infrastructureObject, int infrastructureSize)
    {
        int infrastructureRotation = Random.Range(0, infrastructureRotationsList.Count() - 1);
        GameObject newInfrastructureObject = Instantiate(prefabObject, mouseController.WorldPosition, Quaternion.identity); // is newInfrastructure needed?? use infrastructureRotation ???
        newInfrastructure = newInfrastructureObject.GetComponent<Infrastructure>();
        newInfrastructure.InitiateInfrastructure(infrastructureObject, infrastructureSize, infrastructureRotationsList[infrastructureRotation]);
        boardController.StartBuildState();

        return newInfrastructure; 
    }

    public void BuildInfrastructure(Vector3 worldPosition)
    {
        if (!newInfrastructure.InfrastructureArea.BoardList.Any(n => n.IsUsedByInfrastructure == true) && newInfrastructure.InfrastructureArea.BoardList.Count() == Mathf.Pow(newInfrastructure.InfrastructureSize, 2)) /// implemented as square objects
        {
            gameController.BuildStateAble();
            newInfrastructure.SetInfrastructure();
            boardController.BoardAreaSetAsUsedByInfrastructure(newInfrastructure.InfrastructureArea.BoardList, newInfrastructure);
            boardController.BoardAreaSetAsUsedByInfrastructureArea(newInfrastructure.InfrastructureArea.BoardAreaList, newInfrastructure);
            boardController.EndBuildState();
            AddNewInfrastructureToList();

            newInfrastructure = null;
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
                    gameManager.AddCitizens(); // to move? 
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

    void RemoveInfrastructureFromList(Infrastructure infrastructure)
    {
        if (infrastructure != null)
        {
            switch (infrastructure.InfrastructureObject.ObjectType)
            {
                case ObjectType.House:
                    houseList.Remove(infrastructure);
                    break;

                case ObjectType.Farm:
                    farmList.Remove(infrastructure);
                    break;

                default:
                    Debug.Log("AddNewInfrastructureToList error");
                    break;
            }
        }
    }

    public void DestroyNewInfrastructure()
    {

        gameController.BuildStateAble();
        boardController.QuitBuildState();
        newInfrastructure.DestroyInfrastructure(); 
        newInfrastructure = null;
    }

    public void UpgradeInfrastructure(Infrastructure infrastructure)
    {
        infrastructure.InfrastructureObject.UpgradeObject();
        gameManager.AddCitizens();

    }


    public void DestroyInfrastructure(Infrastructure infrastructure)
    {
        RemoveInfrastructureFromList(infrastructure);
        infrastructure.DestroyInfrastructure();
        // remove value form Gamemanager

    }

    public IEnumerator ImproveInfrastructure(Infrastructure infrastructure) // create switch
    {   
        if (infrastructure != null)
        {

            if (!infrastructure.InfrastructureObject.DevelopeObjectIsAble()) 
            {
                StopCoroutine(ImproveInfrastructure(infrastructure));             
            }
            else // is else needed ?
            {
                yield return new WaitForSecondsRealtime(infrastructure.InfrastructureObject.ImprovementTime);
                infrastructure.InfrastructureObject.DevelopeObject();
                gameManager.AddCitizens(); 
                gameUiMenuController.MenuInfrastructureUpdateUsers(infrastructure); // to check
                StartCoroutine(ImproveInfrastructure(infrastructure));
            }
        }
    }
}

