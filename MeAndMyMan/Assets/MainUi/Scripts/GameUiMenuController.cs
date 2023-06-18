using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class GameUiMenuController : MonoBehaviour
{
    [Header("InfrastructureMenu")]
    [SerializeField] TextMeshProUGUI textAlert;
    [SerializeField] TextMeshProUGUI textInfrastructureDescription;
    [SerializeField] TextMeshProUGUI textInfrastructureArea;
    [SerializeField] TextMeshProUGUI textInfrastructureLevel;
    [SerializeField] TextMeshProUGUI textInfrastructureHealth;
    [SerializeField] TextMeshProUGUI textInfrastructureUsers;
    [SerializeField] TextMeshProUGUI textInfrastructureProduction;
    [SerializeField] TextMeshProUGUI textInfrastructureEnergy;

    [Header("ResourcesMenu")]
    [SerializeField] TextMeshProUGUI textCitizensAmount;
    [SerializeField] TextMeshProUGUI textGoldAmount;
    [SerializeField] TextMeshProUGUI textFoodAmount;
    [SerializeField] TextMeshProUGUI textWoodAmount;
    [SerializeField] TextMeshProUGUI textStoneAmount;
    [SerializeField] TextMeshProUGUI textIronAmount;

    [Header("Values")]
    [SerializeField] MenuUiState menuUiState;
    public MenuUiState MenuUiState { get { return menuUiState; } } //-- ??
    [SerializeField] bool pauseState;
    public bool PauseState { get { return pauseState; } set { pauseState = value; } }
    private Infrastructure infrastructureCurrent;
    public Infrastructure InfrastructureInControl { get { return infrastructureCurrent; } }
    // private Dictionary<MenuUiTabState, ObjectType> menuUiObjectTypeDictionary; 
    // [SerializeField] public Dictionary<MenuUiTabState, ObjectType> MenuUiObjectTypeDictionary { get { return menuUiObjectTypeDictionary; } } 
    GameController gameController;
    GameManager gameManager;
    BoardController boardController;
    InfrastructureController infrastructureController;
    GameUiController gameUiController; 
    MenuUiSectionController menuUiSectionController;
    MenuUiTabController menuUiTabController;

    private void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager; 
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;
        gameUiController = gameController.GameUiController;
        menuUiSectionController = gameController.MenuUiSectionController;
        menuUiTabController = gameController.MenuUiTabController;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Start()
    {
        menuUiState = MenuUiState.UiStateManage; 
        infrastructureCurrent = null;



    }

    private void Update()
    {

        MenuResourcesUpdate(); 

        if(infrastructureCurrent != null) 
        {
            MenuInfrastructureSetValues(infrastructureCurrent);
        }
    }

    public void Manage()
    {
        MenuUiStateChange(MenuUiState.UiStateManage); 
    }

    public void BuildHouse(GameObject gameObject) // ?: maybe somehow to use
    {
        BuildInfrastructure(ObjectType.house);
    }

    public void BuildFarm()
    {
        BuildInfrastructure(ObjectType.farm);
    }

    public void BuildInfrastructure(ObjectType objectType)
    {
        if(gameManager.CheckBuildInfrastructure(objectType, ObjectLevel.Level1))
        {
            infrastructureController.CreateInfrastructure(objectType);
            MenuUiStateChange(MenuUiState.UiStateBuild);
        } 
        else 
        {
            textAlert.text = "Impossible to build " + objectType.ToString() + ". You have not enought resources!"; // TODO: add time
        }
 
    }

    public void About(Infrastructure infrastructure)
    {
        MenuUiStateChange(MenuUiState.UiStateAbout, infrastructure); 
    }

    public void DeteleInfrastructure()
    {
        // TODO: get bool to call gameManager
        infrastructureController.DestroyInfrastructure(infrastructureCurrent); 

        // TODO: gameManager. to implement
    }


    public void RebuildInfrastructure() 
    {
        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureCurrent).Count()))
        {
            infrastructureController.RebuildInfrastructure(infrastructureCurrent); 
        // TODO: gameManager. to implement
        }
        
    }

    public void UpgradeInfrastructure()
    {   
        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureCurrent).Count()))
        {
            // gameManager.CalculateBuildInfrastructure(infrastructure.InfrastructureObject.ObjectType, infrastructure.InfrastructureObject.ObjectLevel);

            infrastructureController.UpgradeInfrastructure(infrastructureCurrent);
        // TODO: gameManager. to implement
        }
    }

    public void ExitBuild(bool isBuilded = false, Infrastructure infrastructureNew = null)
    {
        if(!isBuilded) infrastructureController.DestroyInstantiateInfrastructure();
        gameManager.CalculateBuildInfrastructure(infrastructureNew.InfrastructureObject.ObjectType, infrastructureNew.InfrastructureObject.ObjectLevel);
        MenuUiStateChange(MenuUiState.UiStateManage); 
    }



    private void MenuUiStateChange(MenuUiState menuUiStateNew, Infrastructure infrastructure = null)
    {
        MenuUiStateInfrastructureCheck(infrastructure); 

        if(menuUiState != menuUiStateNew)
        {
            menuUiSectionController.MenuInfrastructureStateManage(menuUiState, menuUiStateNew);
            menuUiState = menuUiStateNew;
        }
    }

    private void MenuUiStateInfrastructureCheck(Infrastructure infrastructure) // ?: move call board Methods to BoardController in update
    {
        if (infrastructureCurrent != infrastructure)
        {   
            if (infrastructureCurrent != null)
            {
                boardController.SetMaterialForListDefault(infrastructureCurrent.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructureCurrent); 
            }

            if(infrastructure != null)
            {
                boardController.SetMaterialForListBlocked(infrastructure.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructure);
            }

            infrastructureCurrent = infrastructure;
        } 
    }

    private void MenuInfrastructureSetValues(Infrastructure infrastructure) 
    {
        textInfrastructureDescription.text = $"{infrastructure.InfrastructureObject.ObjectType}";
        textInfrastructureArea.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}/{infrastructure.InfrastructureObject.AreaSize}";
        textInfrastructureUsers.text = $"{infrastructure.InfrastructureObject.Users}/{infrastructure.InfrastructureObject.UsersMax}";
        textInfrastructureHealth.text = $"{infrastructure.InfrastructureObject.Health}/{infrastructure.InfrastructureObject.HealthMax}";
        textInfrastructureProduction.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}"; // TODO: add method for counting production value 
        textInfrastructureEnergy.text = $"{infrastructure.InfrastructureObject.Energy}/1";
        MenuInfrastructureUpdateLevel(infrastructure);
    }

    private void MenuInfrastructureUpdateLevel(Infrastructure infrastructure)
    {
        int infrastructureLevel = 0;
        
        infrastructureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel;
        textInfrastructureLevel.text = $"{infrastructureLevel}/3";
    }

    private void MenuResourcesUpdate()
    {        
        textCitizensAmount.text = gameManager.ResourcesDictionary[ResourceType.user].ToString();
        textGoldAmount.text = gameManager.ResourcesDictionary[ResourceType.gold].ToString();
        textFoodAmount.text = gameManager.ResourcesDictionary[ResourceType.food].ToString();
        textWoodAmount.text = gameManager.ResourcesDictionary[ResourceType.wood].ToString();
        textStoneAmount.text = gameManager.ResourcesDictionary[ResourceType.stone].ToString();
        textIronAmount.text = gameManager.ResourcesDictionary[ResourceType.iron].ToString();
    }


    private void PauseStateMSet()
    {
        // PauseAllCoroutines(); 
        // PauseAllAnimations(); 
        // PauseAll
    }
}
