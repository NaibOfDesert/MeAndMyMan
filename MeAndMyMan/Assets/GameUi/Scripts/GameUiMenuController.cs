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


    [SerializeField] TextMeshProUGUI textInfrastructureName;
    [SerializeField] TextMeshProUGUI textInfrastructureArea;
    [SerializeField] TextMeshProUGUI textInfrastructureLevel;
    [SerializeField] TextMeshProUGUI textInfrastructureUsers;
    [SerializeField] TextMeshProUGUI textInfrastructureUsersMax;




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
            int infrastrctureLevel = 0;

            infrastructureController.UpgradeInfrastructure(infrastructureMenuState);

            infrastrctureLevel = (int)infrastructureMenuState.InfrastructureObject.ObjectLevel;
            textInfrastructureLevel.text = infrastrctureLevel.ToString();
        }
    }



    public void MenuInfrastructureAble(Infrastructure infrastructure)
    {

        if (infrastructure == null)
        {
            boardController.AbleInfrastructurePlane(infrastructureMenuState);
            menuInfrastructureObject.SetActive(false);
            
            textInfrastructureName.text = null;
            textInfrastructureArea.text = null;
            textInfrastructureLevel.text = null;
            textInfrastructureUsers.text = null;
            textInfrastructureUsersMax.text = null;
            infrastructureMenuState = null;


            return;
        }
        else if (!gameController.BuildState)
        {
            int infrastrctureLevel = 0;

            boardController.AbleInfrastructurePlane(infrastructure); 
            if (infrastructureMenuState != null) boardController.AbleInfrastructurePlane(infrastructureMenuState);

            infrastructureMenuState = infrastructure;
            textInfrastructureName.text = infrastructure.InfrastructureObject.ObjectType.ToString();
            textInfrastructureArea.text = infrastructure.InfrastructureObject.AreaActiveCount.ToString();
            infrastrctureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel; 
            textInfrastructureLevel.text = infrastrctureLevel.ToString();
            textInfrastructureUsers.text = infrastructure.InfrastructureObject.GetUsers().ToString(); // to fix
            textInfrastructureUsersMax.text = infrastructure.InfrastructureObject.GetUsersMax().ToString(); // to fix

            menuInfrastructureObject.SetActive(true);
            gameController.InfrastructureStateAble();
        }


    }
}
