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
    [SerializeField] GameObject textInfrastructureName;


    [SerializeField] TextMeshProUGUI textInfrastructureNameTMPro;
    [SerializeField] TextMeshProUGUI textInfrastructureAreaTMPro;
    [SerializeField] TextMeshProUGUI textInfrastructureLevelTMPro;



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
        menuInfrastructureObject.SetActive(false);


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
            textInfrastructureLevelTMPro.text = infrastrctureLevel.ToString();
        }
    }



    public void MenuInfrastructureAble(Infrastructure infrastructure)
    {

        if (infrastructure == null)
        {
            boardController.AbleInfrastructurePlane(infrastructureMenuState);
            menuInfrastructureObject.SetActive(false);
            
            textInfrastructureNameTMPro.text = null;
            textInfrastructureAreaTMPro.text = null;
            textInfrastructureLevelTMPro.text = null;
            infrastructureMenuState = null;

            return;
        }
        else if (!gameController.BuildState)
        {
            int infrastrctureLevel = 0;

            boardController.AbleInfrastructurePlane(infrastructure); // ??
            boardController.AbleInfrastructurePlane(infrastructureMenuState); // ??

            infrastructureMenuState = infrastructure;
            textInfrastructureNameTMPro.text = infrastructure.InfrastructureObject.ObjectType.ToString(); // ??
            textInfrastructureAreaTMPro.text = infrastructure.InfrastructureObject.AreaActiveCount.ToString();
            infrastrctureLevel = (int)infrastructure.InfrastructureObject.ObjectLevel; 
            textInfrastructureLevelTMPro.text = infrastrctureLevel.ToString();

            menuInfrastructureObject.SetActive(true);
            gameController.InfrastructureStateAble();
        }


    }
}
