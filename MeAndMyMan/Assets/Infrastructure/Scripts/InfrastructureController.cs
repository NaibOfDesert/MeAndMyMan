using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class InfrastructureController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Dictionary<EObjectType, GameObject> prefabDictionary; 
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

    Infrastructure infrastructureNew;
    public Infrastructure InfrastructureNew { get { return infrastructureNew; } }

    GameController gameController;
    GameManager gameManager;
    GameUiMouseController mouseController;
    GameUiMenuController gameUiMenuController;
    GameBoardController boardController;

    // TODO: move board control to UI Controll

    private void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager;
        mouseController = gameController.MouseController;
        gameUiMenuController = gameController.GameUiMenuController;
        boardController = gameController.BoardController;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

        houseList = new List<Infrastructure>();
        farmList = new List<Infrastructure>();
        infrastructureRotationsList = new List<float>() { 0f, 90f, 180f, 270f}; 
    }

    private void Start()
    {

    }
    public Infrastructure CreateInfrastructure(EObjectType objectType)
    {
        ObjectBasic infrastructureObject;
        Infrastructure newInfrastructure; 

        switch (objectType)
        {
            case EObjectType.house:
                infrastructureObject = new House();
                newInfrastructure = InstantiateInfrastructure(housePrefab, infrastructureObject, infrastructureObject.Size);

                break;
            case EObjectType.farm:
                infrastructureObject = new Farm();
                newInfrastructure = InstantiateInfrastructure(farmPrefab, infrastructureObject, infrastructureObject.Size);

                break;
            default:
                Debug.Log("CreateInfrastructure error");
                newInfrastructure = null; 
                break;
        }
        return newInfrastructure;
    }

    private Infrastructure InstantiateInfrastructure(GameObject prefabObject, ObjectBasic infrastructureObject, int infrastructureSize)
    {
        int infrastructureRotation = UnityEngine.Random.Range(0, infrastructureRotationsList.Count() - 1);
        GameObject newInfrastructureObject = Instantiate(prefabObject, mouseController.WorldPosition, Quaternion.identity); // is newInfrastructure needed?? use infrastructureRotation ???
        infrastructureNew = newInfrastructureObject.GetComponent<Infrastructure>();
        infrastructureNew.InitiateInfrastructure(infrastructureObject, infrastructureSize, infrastructureRotationsList[infrastructureRotation]);
        
        
        boardController.StartBuildState(); // TODO: 

        return infrastructureNew; 
    }

    public void DestroyInstantiateInfrastructure()
    {
        boardController.QuitBuildState(); // TODO: 

        infrastructureNew.DestroyInfrastructure();
        infrastructureNew = null;
    }

    public bool BuildNewInfrastructure(Vector3 worldPosition)
    {
        if (!infrastructureNew.InfrastructureArea.BoardList.Any(n => n.IsUsedByInfrastructure == true) && infrastructureNew.InfrastructureArea.BoardList.Count() == Mathf.Pow(infrastructureNew.InfrastructureObject.Size, 2)) /// implemented as square objects
        {
            infrastructureNew.SetInfrastructure();
            StartCoroutine(ImproveInfrastructure(infrastructureNew));
            
            
            boardController.BoardAreaSetAsUsedByInfrastructure(infrastructureNew.InfrastructureArea.BoardList, infrastructureNew); // TODO: 
            boardController.BoardAreaSetAsUsedByInfrastructureArea(infrastructureNew.InfrastructureArea.BoardAreaList, infrastructureNew);
            boardController.EndBuildState();
            
            
            AddNewInfrastructureToList();
            infrastructureNew = null;
            
            return true; 
        } 
        return false;
    }

    void AddNewInfrastructureToList() // ?: manage list, add add ane remove
    {
        if(infrastructureNew != null)
        {
            switch (infrastructureNew.InfrastructureObject.ObjectType)
            {
                case EObjectType.house:
                    houseList.Add(infrastructureNew);
                    break;

                case EObjectType.farm:
                    farmList.Add(infrastructureNew);
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
                case EObjectType.house:  
                    houseList.Remove(infrastructure);
                    break;

                case EObjectType.farm:
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

        gameManager.CalculateDeleteInfrastructure(infrastructure); // TODO: 
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
                WaitUnitlEndOfPause();  //TODO: implemet async
                infrastructure.InfrastructureObject.DevelopeObject();
                gameManager.AddUsers(infrastructure); 

                yield return new WaitForSecondsRealtime(infrastructure.InfrastructureObject.ImprovementTime);
                StartCoroutine(ImproveInfrastructure(infrastructure));
            }
        }
    }

    private async Task WaitUnitlEndOfPause()
    {
       // to add await or wait until?????????????

    }
}

