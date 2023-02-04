using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameUiMenuController : MonoBehaviour
{
    [SerializeField] GameObject menuInfrastructureObject;

    [SerializeField] GameObject buttonHouseBuild;
    [SerializeField] GameObject buttonFarmBuild;
    [SerializeField] GameObject buttonInfrastructureDelete;
    [SerializeField] GameObject buttonInfrastructureRebuildArea;
    [SerializeField] GameObject buttonInfrastructureUpgrade;
    // [SerializeField] GameObject textInfrastructureName;


    [Header("InfrastructureStateMenu")]

    [SerializeField] TextMeshProUGUI textInfrastructureName;
    [SerializeField] TextMeshProUGUI textInfrastructureArea;
    [SerializeField] TextMeshProUGUI textInfrastructureLevel;
    [SerializeField] TextMeshProUGUI textInfrastructureUsers;
    [SerializeField] TextMeshProUGUI textInfrastructureUsersMax;


    [Header("InfrastructureBuildStateMenu")]
    [SerializeField] TextMeshProUGUI textInfrastructureBuildName;





    Infrastructure infrastructureMenuState;




    GameController gameController;
    MouseController mouseController;
    BoardController boardController;
    InfrastructureController infrastructureController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;



    }

    void Start()
    {
        infrastructureMenuState = null;
    }

    void Update()
    {
        
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
        infrastructureController.DestroyInfrastructure(infrastructureMenuState);
        MenuInfrastructureAble(null);


    }

    public void RebuildInfrastructure()
    {



    }

    public void UpgradeInfrastructure()
    {
        if (infrastructureMenuState != null)
        {
            infrastructureController.UpgradeInfrastructure(infrastructureMenuState);
            MenuInfrastructureUpdatelevel(infrastructureMenuState); 
        }
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

            menuInfrastructureObject.SetActive(false);
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
                MenuInfrastructureUpdatelevel(infrastructure);
                MenuInfrastructureUpdateUsers(infrastructure);
                textInfrastructureUsersMax.text = infrastructure.InfrastructureObject.GetUsersMax().ToString();
                menuInfrastructureObject.SetActive(true);
            }
        }
    }

    public void MenuInfrastructureUpdateUsers(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            textInfrastructureUsers.text = infrastructure.InfrastructureObject.GetUsers().ToString();
        }
    }

    public void MenuInfrastructureUpdatelevel(Infrastructure infrastructure)
    {
        if (gameController.InfrastructureState && infrastructure == infrastructureMenuState)
        {
            int infrastructureLevel = 0;

            infrastructureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel;
            textInfrastructureLevel.text = infrastructureLevel.ToString();
        }
    }

}
