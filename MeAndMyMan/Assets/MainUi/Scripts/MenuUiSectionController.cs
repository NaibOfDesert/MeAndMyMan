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
    [SerializeField] MenuUiSection infrastructureManageSection;
    [SerializeField] MenuUiSection infrastructureAboutSection;
    [SerializeField] MenuUiSection infrastructureBuildSection;
    [SerializeField] List<IMenuUi> menuUiSectionList;



    void Start()
    {
        menuUiSectionList = new List<IMenuUi>();

        infrastructureAboutSection.SetSectionAble();
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

    public void MenuInfrastructureStateManage(MenuUiStates menuUiState)
    {
        switch (menuUiState)
        {
            // case MenuUiStates.infrastructureManageState: 
            //     {
            //         InfrastructureManageSectionAble();
            //         break;
            //     }
            case MenuUiStates.infrastructureManageState: 
            case MenuUiStates.infrastructureAboutState:
                {
                    InfrastructureAboutSectionAble();
                    InfrastructureManageSectionAble();
                    MenuInformationAble();
                    break;

                }
            case MenuUiStates.infrastructureBuildState:
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
