using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuUiSectionController : MonoBehaviour
{
    [Header("MenuSectionObjects")]
    [SerializeField] MenuUiSection optionsSection;
    [SerializeField] MenuUiSection gameSection;
    [SerializeField] MenuUiSection informationDescriptionSection;
    [SerializeField] MenuUiSection informationValueSection;
    [SerializeField] MenuUiSection infrastructureManageSection;
    [SerializeField] MenuUiSection infrastructureAboutSection;
    [SerializeField] MenuUiSection infrastructureBuildSection;
    [SerializeField] List<MenuUiSection> menuUiSectionList;



    void Start()
    {
        menuUiSectionList = new List<MenuUiSection>();

        infrastructureAboutSection.SetSectionAble();
        infrastructureBuildSection.SetSectionAble();
        informationDescriptionSection.SetSectionAble();
        informationValueSection.SetSectionAble();
    }


    void Update()
    {
        
    }

  

    public void AddToUiList(MenuUiSection menuUiSection)
    {
        menuUiSectionList.Add(menuUiSection);
    }


    void MenuInformationAble()
    {
        informationDescriptionSection.SetSectionAble();
        informationValueSection.SetSectionAble();
    }

    void InfrastructureAboutSectionAble()
    {
        infrastructureAboutSection.SetSectionAble();
    }

    void InfrastructureManageSectionAble()
    {
        infrastructureManageSection.SetSectionAble();
    }

    void InfrastructureBuildSectionAble()
    {
        infrastructureBuildSection.SetSectionAble();
    }

    public void MenuInfrastructureStateManage(MenuUiState menuUiState)
    {
        switch (menuUiState)
        {
            // case MenuUiStates.infrastructureManageState: 
            //     {
            //         InfrastructureManageSectionAble();
            //         break;
            //     }
            case MenuUiState.infrastructureManageState: 
            case MenuUiState.infrastructureAboutState:
                {
                    InfrastructureManageSectionAble();
                    InfrastructureAboutSectionAble();
                    MenuInformationAble();
                    break;

                }
            case MenuUiState.infrastructureBuildState:
                {
                    InfrastructureManageSectionAble();
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
