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

    [SerializeField] MenuUiState menuUiState;
    public MenuUiState MenuUiState { get { return menuUiState; } } //-- ??
    [SerializeField] bool pauseState;
    public bool PauseState { get { return pauseState; } set { pauseState = value; } }

    private Infrastructure infrastructureInControl;
    public Infrastructure InfrastructureInControl { get { return infrastructureInControl; } }
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
        infrastructureInControl = null;
        menuUiState = MenuUiState.infrastructureManageState;


    }

    void Update()
    {
        MenuResourcesUpdate(); 

        if(infrastructureInControl != null) 
        {
            MenuInfrastructureSetValues(infrastructureInControl);
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
        ChangeMenuUiState(MenuUiState.infrastructureBuildState);
        infrastructureController.CreateInfrastructure(objectType);
        // gameManager. to implement


    }
    public void DeteleInfrastructure()
    {
        ChangeMenuUiState(MenuUiState.infrastructureAboutState);
        infrastructureController.DestroyInfrastructure(infrastructureInControl);
        // gameManager. to implement

    }


    public void RebuildInfrastructure() 
    {
        if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureInControl).Count()))
        {
            ChangeMenuUiState(MenuUiState.infrastructureAboutState);
            infrastructureController.UpgradeInfrastructure(infrastructureInControl);
            // gameManager. to implement
        }
        
    }

    public void UpgradeInfrastructure()
    {   
        if (infrastructureInControl != null)
        {
            infrastructureController.UpgradeInfrastructure(infrastructureInControl);
            MenuInfrastructureUpdateLevel(infrastructureInControl); 
        }
    }

    public void ChangeMenuUiState(MenuUiState menuUiState)
    {
        menuUiSectionController.MenuInfrastructureStateManage(menuUiState);
        if(menuUiState == MenuUiState.infrastructureState)
        {
            if (this.menuUiState == menuUiState)
            {
                this.menuUiState = MenuUiState.infrastructureManageState; /// INFO: infrastructureManageState is basic state
                return; 
            }
            this.menuUiState = menuUiState;
        }

    }

    public void MenuInformationSet(Infrastructure infrastructure)
    {
        if(infrastructure == null)
        {
            infrastructureInControl = null;
            MenuInformationSetDefault();
            return;
        }

        if (infrastructure != infrastructureInControl)
        {   
            if (menuUiState == MenuUiState.infrastructureManageState) 
            {
                ChangeMenuUiState(MenuUiState.infrastructureAboutState);
            }        
            else return; 

            if (infrastructureInControl != null)
            {
                boardController.SetMaterialForListDefault(infrastructureInControl.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructureInControl); 
            }
            infrastructureInControl = infrastructure;

            boardController.SetMaterialForListBlocked(infrastructure.InfrastructureArea.BoardAreaBlockedList);
            boardController.AbleInfrastructurePlane(infrastructure);
            
            MenuInfrastructureSetValues(infrastructure);
        } 
    }

    public void MenuInformationSetDefault()
    {
        textInfrastructureName.text = $"0";
        textInfrastructureArea.text = $"0/0";
        textInfrastructureUsers.text = $"0/0";
        textInfrastructureHealth.text = $"0/0";
        textInfrastructureProduction.text = $"0";
        textInfrastructureEnergy.text = $"0/1";
        textInfrastructureLevel.text = $"0/3";
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
