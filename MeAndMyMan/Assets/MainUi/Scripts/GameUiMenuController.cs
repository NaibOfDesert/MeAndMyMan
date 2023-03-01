using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameUiMenuController : MonoBehaviour
{
    [Header("MenuSectionObjects")]
    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject gameUiAbout;
    [SerializeField] GameObject gameUiBuild;
    [SerializeField] GameObject gameUiBuildExit;

    [Header("InfrastructureStateMenu")]
    [SerializeField] TextMeshProUGUI textInfrastructureName;
    [SerializeField] TextMeshProUGUI textInfrastructureArea;
    [SerializeField] TextMeshProUGUI textInfrastructureLevel;
    [SerializeField] TextMeshProUGUI textInfrastructureUsers;
    [SerializeField] TextMeshProUGUI textInfrastructureUsersMax;

    [Header("InfrastructureBuildStateMenu")]
    [SerializeField] TextMeshProUGUI textInfrastructureBuildName;

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
        gameUiAbout.SetActive(false);
        gameUiBuildExit.SetActive(false);
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
            MenuInfrastructureAble(null);
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

    public void MenuInfrastructureAble(Infrastructure infrastructure)
    {
        if (infrastructure == null)
        {
            gameController.InfrastructureStateAble();
            boardController.SetMaterialForListDefault(infrastructureMenuState.InfrastructureArea.BoardAreaBlockedList);
            boardController.AbleInfrastructurePlane(infrastructureMenuState);
            
            textInfrastructureName.text = null;
            textInfrastructureArea.text = null;
            textInfrastructureLevel.text = null;
            textInfrastructureUsers.text = null;
            textInfrastructureUsersMax.text = null;
            infrastructureMenuState = null;

            gameUiAbout.SetActive(false); // change to method
            return;
        }
        else if (!gameController.BuildState)
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

                textInfrastructureName.text = infrastructure.InfrastructureObject.ObjectType.ToString();
                textInfrastructureArea.text = infrastructure.InfrastructureObject.AreaActiveCount.ToString();
                MenuInfrastructureUpdateLevel(infrastructure);
                MenuInfrastructureUpdateUsers(infrastructure);
                textInfrastructureUsersMax.text = infrastructure.InfrastructureObject.UsersMax.ToString();
                gameUiAbout.SetActive(true); // change to method
            }
        }
    }

    public void MenuInfrastructureUpdateUsers(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            textInfrastructureUsers.text = infrastructure.InfrastructureObject.Users.ToString();
        }
    }

    public void MenuInfrastructureUpdateLevel(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            int infrastructureLevel = 0;

            infrastructureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel;
            textInfrastructureLevel.text = infrastructureLevel.ToString();
        }
    }

    public void PauseStateMenusAble()
    {
        // PauseAllCoroutines(); 
        // PauseAllAnimations(); 
        // PauseAll
    }
}
