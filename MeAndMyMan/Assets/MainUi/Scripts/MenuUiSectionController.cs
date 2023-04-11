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
        MenuListAble(MenuUiState.infrastructureState);
        MenuListAble(MenuUiState.informationState);
        MenuListAble(MenuUiState.infrastructureManageState);
    }


    void Update()
    {
        
    }

  

    public void AddToUiList(MenuUiSection menuUiSection)
    {
        if(menuUiSection.MenuUiState != MenuUiState.noneState)
        menuUiSectionList.Add(menuUiSection);
    }

    public void MenuInfrastructureStateManage(MenuUiState menuUiStateCurrent, MenuUiState menuUiStateNew)
    {
       MenuListAble(menuUiStateCurrent); 
       MenuListAble(menuUiStateNew); 
    }

    private void MenuListAble(MenuUiState menuUiState)
    {
        foreach(var s in menuUiSectionList){
            var menuUiStateString = s.MenuUiState.ToString(); 
            var menuUiStateList= String.Concat(menuUiStateString.Where(l => !char.IsWhiteSpace(l))).Split(",").ToList(); 
            
            if(menuUiStateList.Any(m => m == menuUiState.ToString()))
            {
                s.SetSectionAble();
            }
        }  
    }


}
