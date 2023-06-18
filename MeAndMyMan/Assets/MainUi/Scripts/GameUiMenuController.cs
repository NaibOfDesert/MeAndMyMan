using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class GameUiMenuController : MonoBehaviour
{
    [Header("InfrastructureStateMenu")]
    [SerializeField] TextMeshProUGUI textInfrastructureName;
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
    [SerializeField] MenuUiSectionState menuUiState;
    public MenuUiSectionState MenuUiState { get { return menuUiState; } } //-- ??
    [SerializeField] bool pauseState;
    public bool PauseState { get { return pauseState; } set { pauseState = value; } }
    private Infrastructure infrastructureInAboutState;
    public Infrastructure InfrastructureInControl { get { return infrastructureInAboutState; } }
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
        infrastructureInAboutState = null;
        // menuUiState = MenuUiSectionState.infrastructureCreateState;

        // menuUiObjectTypeDictionary = new Dictionary<MenuUiTabState, ObjectType>();
        // GenereteUiObjectRelation();
    }

    private void Update()
    {
        Debug.Log("value to compare" + gameManager.testValue1);
        Debug.Log("value to check" + gameManager.testValue);

        MenuResourcesUpdate(); 

        if(infrastructureInAboutState != null) 
        {
            MenuInfrastructureSetValues(infrastructureInAboutState);
        }
    }

    public void BuildHouse()
    {
        BuildInfrastructure(ObjectType.house);
    }

    public void BuildFarm()
    {
        BuildInfrastructure(ObjectType.farm);
    }

    public void BuildInfrastructure(ObjectType objectType)
    {
        infrastructureController.CreateInfrastructure(objectType); // TODO: return bool, is bool call gameManager

        // gameManager. to implement


    }
    public void DeteleInfrastructure()
    {
        // TODO: get bool to call gameManager
        infrastructureController.DestroyInfrastructure(infrastructureInAboutState); 

        // TODO: gameManager. to implement
    }


    public void RebuildInfrastructure() 
    {
        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureInAboutState).Count()))
        {
            infrastructureController.RebuildInfrastructure(infrastructureInAboutState); 
        // TODO: gameManager. to implement
        }
        
    }

    public void UpgradeInfrastructure()
    {   
        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureInAboutState).Count()))
        {
            infrastructureController.UpgradeInfrastructure(infrastructureInAboutState);
        // TODO: gameManager. to implement
        }
    }

    public void MenuUiStateChange(MenuUiSectionState menuUiState, Infrastructure infrastructure = null)
    {
        MenuUiStateInfrastructureCheck(infrastructure); 

        if(this.menuUiState != menuUiState)
        {
            menuUiSectionController.MenuInfrastructureStateManage(this.menuUiState, menuUiState);
            this.menuUiState = menuUiState;
        }
    }

    private void MenuUiStateInfrastructureCheck(Infrastructure infrastructure) // ?: move call board Methods to BoardController in update
    {
        if (infrastructureInAboutState != infrastructure)
        {   
            if (infrastructureInAboutState != null)
            {
                boardController.SetMaterialForListDefault(infrastructureInAboutState.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructureInAboutState); 
            }

            if(infrastructure != null)
            {
                boardController.SetMaterialForListBlocked(infrastructure.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructure);
            }

            infrastructureInAboutState = infrastructure;
        } 
    }

    private void MenuInfrastructureSetValues(Infrastructure infrastructure) 
    {
        textInfrastructureName.text = $"{infrastructure.InfrastructureObject.ObjectType}";
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
