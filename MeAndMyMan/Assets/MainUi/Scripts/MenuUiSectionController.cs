using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class MenuUiSectionController : MonoBehaviour
{
    [Header("MenuSectionObjects")]
    [SerializeField] List<MenuUiSection> menuUiSectionList;

    void Awake()
    {
        menuUiSectionList = new List<MenuUiSection>();
    }

    void Start()
    {
        // MenuListAble(MenuUiSectionState.infrastructureState);
        // MenuListAble(MenuUiSectionState.informationState);
        // MenuListAble(MenuUiSectionState.infrastructureCreateState);
    }


    void Update()
    {
        
    }

  

    // public void AddToUiList(MenuUiSection menuUiSection)
    // {
    //     if(menuUiSection.MenuUiSectionState != MenuUiSectionState.noneState)
    //     menuUiSectionList.Add(menuUiSection);
    // }

    public void MenuInfrastructureStateManage(MenuUiSectionState menuUiStateCurrent, MenuUiSectionState menuUiStateNew)
    {
       MenuListAble(menuUiStateCurrent); 
       MenuListAble(menuUiStateNew); 
    }

    private void MenuListAble(MenuUiSectionState menuUiState)
    {
        foreach(var s in menuUiSectionList)
        {
            if(s.MenuUiStatesList.Any(m => m == menuUiState.ToString()))
            {
                s.SetSectionAble();
            }
        }  
    }


}
