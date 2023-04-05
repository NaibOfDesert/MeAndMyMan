using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class InfrastructureController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject housePrefab;
    public GameObject HousePrefab { get { return housePrefab; } } // TODO: check is needed? 

    List<Infrastructure> houseList;

    public List<Infrastructure> HouseList { get { return houseList; } }

    [SerializeField] GameObject farmPrefab;
    public GameObject FarmPrefab { get { return farmPrefab; } } // TODO: check is needed? 

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
        if(!gameManager.CheckBuildInfrastructure(objectType, ObjectLevel.Level1))
        {
            // throw new System.ArgumentOutOfRangeException(); // check
            return; 
        }

        Object infrastructureObject;
        // gameController.BuildStateAble();

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

    public void DestroyInstantiateInfrastructure()
    {
        boardController.QuitBuildState();
        newInfrastructure.DestroyInfrastructure();
        newInfrastructure = null;
    }

    public bool BuildNewInfrastructure(Vector3 worldPosition)
    {
        if (!newInfrastructure.InfrastructureArea.BoardList.Any(n => n.IsUsedByInfrastructure == true) && newInfrastructure.InfrastructureArea.BoardList.Count() == Mathf.Pow(newInfrastructure.InfrastructureSize, 2)) /// implemented as square objects
        {
            gameManager.CalculateBuildInfrastructure(newInfrastructure.InfrastructureObject.ObjectType, newInfrastructure.InfrastructureObject.ObjectLevel);
            newInfrastructure.SetInfrastructure();
            StartCoroutine(ImproveInfrastructure(newInfrastructure));
            boardController.BoardAreaSetAsUsedByInfrastructure(newInfrastructure.InfrastructureArea.BoardList, newInfrastructure);
            boardController.BoardAreaSetAsUsedByInfrastructureArea(newInfrastructure.InfrastructureArea.BoardAreaList, newInfrastructure);
            boardController.EndBuildState();
            AddNewInfrastructureToList();
            newInfrastructure = null;
            
            return true; 
        } 
        return false;
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

    public void UpgradeInfrastructure(Infrastructure infrastructure)
    {
        infrastructure.InfrastructureObject.UpgradeObject();
        gameManager.CalculateBuildInfrastructure(infrastructure.InfrastructureObject.ObjectType, infrastructure.InfrastructureObject.ObjectLevel);
        StartCoroutine(ImproveInfrastructure(infrastructure)); // stop all coroutine
    }

    public void RebuildInfrastructure(Infrastructure infrastructure)
    {
        //++
        // to add in infArea change Lists

        // callout board change
        



    }

    public List<Tile> CheckAreaToRebuildInfrastructure(Infrastructure infrastructure)
    {
        List<Tile> areaListToRebuild = infrastructure.InfrastructureArea.BoardAreaBlockedList;
        areaListToRebuild.RemoveAll(n => n.IsUsedByInfrastructure == true); 
        return areaListToRebuild; 
    }

    public void DestroyInfrastructure(Infrastructure infrastructure)
    {
        try
        {
            StopCoroutine(ImproveInfrastructure(infrastructure));
        }
        catch 
        {
            //TODO: event system
        }

        gameManager.CalculateDeleteInfrastructure(infrastructure);
        boardController.SetDefaultInfrastructure(infrastructure.InfrastructureArea.BoardList);
        boardController.SetDefaultInfrastructureArea(infrastructure.InfrastructureArea.BoardAreaList);
        RemoveInfrastructureFromList(infrastructure);
        infrastructure.DestroyInfrastructure();
    }

    public IEnumerator ImproveInfrastructure(Infrastructure infrastructure) // create switch
    {   
        if (infrastructure != null)
        {
            
            if (!infrastructure.InfrastructureObject.DevelopeObjectIsAble()) 
            {
                StopCoroutine(ImproveInfrastructure(infrastructure));             
            }
            else 
            {
                WaitUnitlEndOfApuse();  //TODO: implemet async
                infrastructure.InfrastructureObject.DevelopeObject();
                gameManager.AddUsers(infrastructure); 

                yield return new WaitForSecondsRealtime(infrastructure.InfrastructureObject.ImprovementTime);
                StartCoroutine(ImproveInfrastructure(infrastructure));
            }
        }
    }

    private async Task WaitUnitlEndOfApuse()
    {
       // to add await or wait until?????????????

    }
}

