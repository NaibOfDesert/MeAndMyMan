using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuUiSectionController : MonoBehaviour, IMenuUiController
{
    [Header("MenuSectionObjects")]
    [SerializeField] MenuUiSection optionsSection;
    [SerializeField] MenuUiSection gameSection;
    [SerializeField] MenuUiSection informationDescriptionSection;
    [SerializeField] MenuUiSection informationValueSection;
    [SerializeField] MenuUiSection infrastructureSection;
    [SerializeField] MenuUiSection infrastructureManageSection;
    [SerializeField] MenuUiSection infrastructureBuildSection;

    [SerializeField] List<IMenuUi> menuUiSectionList;



    void Start()
    {
        menuUiSectionList = new List<IMenuUi>();

        infrastructureSection.SetSectionAble();
        infrastructureBuildSection.SetSectionAble();
        informationDescriptionSection.SetSectionAble();
        informationValueSection.SetSectionAble();
    }


    void Update()
    {
        
    }

  

    public void AddToUiList(IMenuUi menuUiSection)
    {
        menuUiSectionList.Add(menuUiSection);
    }


    void MenuInformationAble()
    {
        informationDescriptionSection.SetSectionAble();
        informationValueSection.SetSectionAble();
    }

    void InfrastructureSectionAble()
    {
        infrastructureSection.SetSectionAble();
    }

    void InfrastructureManageSectionAble()
    {
        infrastructureManageSection.SetSectionAble();
    }

    void InfrastructureBuildSectionAble()
    {
        infrastructureBuildSection.SetSectionAble();
    }

    public void MenuInfrastructureStateManage(MenuUiStates menuUiState)
    {
        switch (menuUiState)
        {

            case MenuUiStates.infrastructureState:
                {
                    InfrastructureManageSectionAble();
                    break;

                }

            case MenuUiStates.infrastructureManageState:
                {
                    InfrastructureSectionAble();
                    InfrastructureManageSectionAble();
                    MenuInformationAble();
                    break;
                }
            case MenuUiStates.infrastructureBuildState:
                {
                    InfrastructureSectionAble();
                    InfrastructureBuildSectionAble();
                    break;

                }
            default:
                {
                    break;
                }



        }
    }




}
