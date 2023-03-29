using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameUiMenuController : MonoBehaviour
{
    [Header("MenuSectionObjects")]
    [SerializeField] MenuUiSection optionsSection;
    [SerializeField] MenuUiSection gameSection;
    [SerializeField] MenuUiSection informationDescriptionSection;
    [SerializeField] MenuUiSection informationValueSection;
    [SerializeField] MenuUiSection infrastructureSection;
    [SerializeField] MenuUiSection infrastructureBuildSection;
    [SerializeField] MenuUiSection infrastructureBuildExitSection;

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

    Infrastructure infrastructureMenuState;
    GameController gameController;
    GameManager gameManager;
    BoardController boardController;
    InfrastructureController infrastructureController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameManager = gameController.GameManager; 
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;
    }

    void Start()
    {
        infrastructureMenuState = null;
        infrastructureSection.SetSectionEnable();
        infrastructureBuildExitSection.SetSectionEnable();
        informationDescriptionSection.SetSectionEnable();
        informationValueSection.SetSectionEnable();
    }

    void Update()
    {
        MenuResourcesUpdate(); 

        if(infrastructureMenuState != null) 
        {

        }
    }

    public void BuildHouse()
    {
        if (!gameController.BuildState)
        {
            infrastructureController.CreateInfrastructure(ObjectType.House);
        }
    }

    public void BuildFarm()
    {
        if (!gameController.BuildState)
        {
            infrastructureController.CreateInfrastructure(ObjectType.Farm);
        }
    }

    public void DeteleInfrastructure()
    {
        if (infrastructureMenuState != null)
        {
            infrastructureController.DestroyInfrastructure(infrastructureMenuState);
            MenuInformationAble(null);
        }
    }

    public void RebuildInfrastructure()
    {

        if (infrastructureMenuState != null)
        {
            if (gameManager.CheckRebuildInfrastructure(infrastructureController.CheckAreaToRebuildInfrastructure(infrastructureMenuState).Count())) {
                infrastructureController.UpgradeInfrastructure(infrastructureMenuState);
                // gameManager. to implement
            }

        }
        
    }

    public void UpgradeInfrastructure()
    {   
        if (infrastructureMenuState != null)
        {
            infrastructureController.UpgradeInfrastructure(infrastructureMenuState);
            MenuInfrastructureUpdateLevel(infrastructureMenuState); 
        }
    }



    public void MenuInformationAble(Infrastructure infrastructure)
    {
        if (infrastructure == null)
        {
            gameController.InfrastructureStateAble();
            boardController.SetMaterialForListDefault(infrastructureMenuState.InfrastructureArea.BoardAreaBlockedList);
            boardController.AbleInfrastructurePlane(infrastructureMenuState);
            
            textInfrastructureName.text = null;
            textInfrastructureArea.text = null;
            textInfrastructureLevel.text = null;
            textInfrastructureHealth.text = null;
            textInfrastructureUsers.text = null;
            textInfrastructureProduction.text = null;
            textInfrastructureEnergy.text = null;
            infrastructureMenuState = null;

            infrastructureSection.SetSectionEnable(); // change to method
            return;
        }
        else if (!gameController.BuildState) // add possibilty to able section i build mode
        {
            if (!gameController.InfrastructureState) gameController.InfrastructureStateAble();
            if (infrastructure != infrastructureMenuState)
            {   
                if (infrastructureMenuState != null)
                {
                    boardController.SetMaterialForListDefault(infrastructureMenuState.InfrastructureArea.BoardAreaBlockedList);
                    boardController.AbleInfrastructurePlane(infrastructureMenuState); 
                }
                infrastructureMenuState = infrastructure;

                boardController.SetMaterialForListBlocked(infrastructure.InfrastructureArea.BoardAreaBlockedList);
                boardController.AbleInfrastructurePlane(infrastructure);

                textInfrastructureName.text = $"{infrastructure.InfrastructureObject.ObjectType}";
                textInfrastructureArea.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}/{infrastructure.InfrastructureObject.AreaSize}";
                MenuInfrastructureUpdateLevel(infrastructure);
                textInfrastructureHealth.text = $"{infrastructure.InfrastructureObject.Health}/{infrastructure.InfrastructureObject.HealthMax}";
                textInfrastructureProduction.text = $"{infrastructure.InfrastructureObject.AreaActiveCount}"; // TODO: add method for counting production value 
                textInfrastructureEnergy.text = $"{infrastructure.InfrastructureObject.Energy}/1";
                MenuInfrastructureUpdateUsers(infrastructure);
                infrastructureSection.SetSectionAble();
            }
        }
    }

    public void MenuInfrastructureUpdateUsers(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            textInfrastructureUsers.text = $"{infrastructure.InfrastructureObject.Users}/{infrastructure.InfrastructureObject.UsersMax}";
        }
    }

    public void MenuInfrastructureUpdateLevel(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            int infrastructureLevel = 0;

            infrastructureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel;
            textInfrastructureLevel.text = $"{infrastructureLevel}/3";
        }
    }

    public void MenuInformationEnable()
    {
        informationDescriptionSection.SetSectionEnable();
        informationValueSection.SetSectionEnable();
    }


    public void MenuInfrastructureBuildAble()
    {

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


    public void PauseStateMenusAble()
    {
        // PauseAllCoroutines(); 
        // PauseAllAnimations(); 
        // PauseAll
    }
}
