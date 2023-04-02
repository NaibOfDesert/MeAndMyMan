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
    [SerializeField] TextMeshProUGUI textWorkersAmount;
    [SerializeField] TextMeshProUGUI textGoldAmount;
    [SerializeField] TextMeshProUGUI textFoodAmount;
    [SerializeField] TextMeshProUGUI textWoodAmount;
    [SerializeField] TextMeshProUGUI textStoneAmount;
    [SerializeField] TextMeshProUGUI textIronAmount;

    [SerializeField] MenuUiStates menuUiState;
    public MenuUiStates MenuUiState { get { return menuUiState; } set { menuUiState = value; } } //-- ??

    [SerializeField] bool pauseState;
    public bool PauseState { get { return pauseState; } set { pauseState = value; } }


    Infrastructure infrastructureMenu;
    GameController gameController;
    GameManager gameManager;
    BoardController boardController;
    InfrastructureController infrastructureController;
    GameUiController gameUiController; 
    MenuUiSectionController menuUiSectionController;
    MenuUiTabController menuUiTabController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager; 
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;
        gameUiController = gameController.GameUiController;
        menuUiSectionController = gameController.MenuUiSectionController;
        menuUiTabController = gameController.MenuUiTabController;
    }

    void Start()
    {
        infrastructureMenu = null;
        ChangeMenuUiState(MenuUiStates.infrastructureState);
    }

    void Update()
    {
        MenuResourcesUpdate(); 

        if(infrastructureMenu != null) 
        {
            MenuInfrastructureSetValues(infrastructureMenu);
        }
    }

    public void BuildHouse()
    {
        BuildInfrastructure(ObjectType.House);
    }

    public void BuildFarm()
    {
        BuildInfrastructure(ObjectType.Farm);
    }

    public void BuildInfrastructure(ObjectType objectType)
    {
        ChangeMenuUiState(MenuUiStates.infrastructureBuildState);
        infrastructureController.CreateInfrastructure(objectType);

    }
    public void DeteleInfrastructure()
    {
        ChangeMenuUiState(MenuUiStates.infrastructureManageState);
        infrastructureController.DestroyInfrastructure(infrastructureMenu);
    }


    public void RebuildInfrastructure()
    {

        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureMenu).Count()))
        {
            infrastructureController.UpgradeInfrastructure(infrastructureMenu);
            // gameManager. to implement
        }
        
    }

    public void UpgradeInfrastructure()
    {   
        if (infrastructureMenu != null)
        {
            infrastructureController.UpgradeInfrastructure(infrastructureMenu);
            MenuInfrastructureUpdateLevel(infrastructureMenu); 
        }
    }

    public void ChangeMenuUiState(MenuUiStates menuUiState)
    {
        if (this.menuUiState == menuUiState)
        {
            this.menuUiState = MenuUiStates.infrastructureState;
            menuUiSectionController.MenuInfrastructureStateManage(menuUiState);
            return; 
        }
        this.menuUiState = menuUiState;

    }

    public void MenuInformationSet(Infrastructure infrastructure)
    {
        // TODO: to update
        if (menuUiState != MenuUiStates.infrastructureManageState) ChangeMenuUiState(MenuUiStates.infrastructureManageState);
        if (infrastructureMenu != infrastructure && infrastructureMenu != null) SetBoardDefault(infrastructureMenu);

        infrastructureMenu = infrastructure;
        MenuInfrastructureSetValues(infrastructure);
    }

    public void SetBoardDefault(Infrastructure infrastructure)
    {
        boardController.SetMaterialForListDefault(infrastructure.InfrastructureArea.BoardAreaBlockedList);
        boardController.AbleInfrastructurePlane(infrastructure);
    }

    public void MenuInfrastructureSetValues(Infrastructure infrastructure)
    {
        textInfrastructureName.text = $"{infrastructure.InfrastructureObject.ObjectType}";
        textInfrastructureArea.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}/{infrastructure.InfrastructureObject.AreaSize}";
        textInfrastructureUsers.text = $"{infrastructure.InfrastructureObject.Users}/{infrastructure.InfrastructureObject.UsersMax}";
        textInfrastructureHealth.text = $"{infrastructure.InfrastructureObject.Health}/{infrastructure.InfrastructureObject.HealthMax}";
        textInfrastructureProduction.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}"; // TODO: add method for counting production value 
        textInfrastructureEnergy.text = $"{infrastructure.InfrastructureObject.Energy}/1";
        MenuInfrastructureUpdateLevel(infrastructure);
    }

    public void MenuInfrastructureUpdateLevel(Infrastructure infrastructure)
    {
        int infrastructureLevel = 0;

        infrastructureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel;
        textInfrastructureLevel.text = $"{infrastructureLevel}/3";
    }

    void MenuResourcesUpdate()
    {
        textCitizensAmount.text = gameManager.CitizensAmount.ToString();
        textWorkersAmount.text = gameManager.WorkersAmount.ToString();
        textGoldAmount.text = gameManager.GoldAmount.ToString();
        textFoodAmount.text = gameManager.FoodAmount.ToString();
        textWoodAmount.text = gameManager.WoodAmount.ToString();
        textStoneAmount.text = gameManager.StoneAmount.ToString();
        textIronAmount.text = gameManager.IronAmount.ToString();
    }


    public void PauseStateMSet()
    {
        // PauseAllCoroutines(); 
        // PauseAllAnimations(); 
        // PauseAll
    }
}
