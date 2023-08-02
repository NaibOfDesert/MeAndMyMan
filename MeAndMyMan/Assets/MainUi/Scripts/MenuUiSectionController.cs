using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class MenuUiSectionController : MonoBehaviour
{
    [Header("MenuSectionObjects")]
    [SerializeField] List<MenuUiSectionActive> menuUiSectionList;

    void Awake()
    {
        menuUiSectionList = new List<MenuUiSectionActive>();
    }

    void Start()
    {
        MenuInfrastructureStateManage(EMenuUiState.UiStateAbout, EMenuUiState.UiStateBuild);


    }


    void Update()
    {
        
    }


    public void AddToUiList(MenuUiSectionActive menuUiSection)
    {
        menuUiSectionList.Add(menuUiSection);
    }
    public void MenuInfrastructureStateManage(EMenuUiState menuUiTypeCurrent, EMenuUiState menuUiTypeNew)
    {
        var sections = menuUiSectionList.Where(s => s.MenuUiState == menuUiTypeCurrent || s.MenuUiState == menuUiTypeNew); 

        foreach(var s in sections)
        {
            s.SetSectionAble();
        }
    }
}
