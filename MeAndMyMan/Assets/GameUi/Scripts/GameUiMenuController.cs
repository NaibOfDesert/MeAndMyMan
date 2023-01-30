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
    Infrastructure infrastructureMenuState; 




    GameController gameController;
    MouseController mouseController;
    InfrastructureController infrastructureController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController;
        textInfrastructureNameTMPro = textInfrastructureName.GetComponent<TextMeshProUGUI>();



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



    }



    public void MenuInfrastructureAble(Infrastructure infrastructure)
    {
        infrastructureMenuState = infrastructure;


        if (infrastructure == null)
        {
            menuInfrastructureObject.SetActive(false);
            return;
        }
        else if (!gameController.BuildState)
        {
            textInfrastructureNameTMPro.text = infrastructure.InfrastructureObject.ObjectType.ToString(); // ??

            menuInfrastructureObject.SetActive(true);
            gameController.InfrastructureStateAble();

        }


    }
}
