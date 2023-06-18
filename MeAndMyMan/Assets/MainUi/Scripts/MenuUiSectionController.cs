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
        MenuInfrastructureStateManage(MenuUiState.UiStateAbout, MenuUiState.UiStateBuild);


    }


    void Update()
    {
        
    }


    public void AddToUiList(MenuUiSectionActive menuUiSection)
    {
        menuUiSectionList.Add(menuUiSection);
    }
    public void MenuInfrastructureStateManage(MenuUiState menuUiTypeCurrent, MenuUiState menuUiTypeNew)
    {
                Debug.Log(menuUiSectionList.Count());
        var sections = menuUiSectionList.Where(s => s.MenuUiState == menuUiTypeCurrent || s.MenuUiState == menuUiTypeNew); 
        Debug.Log(sections.Count()); 

        foreach(var s in sections)
        {
            Debug.Log(s.MenuUiState); 
            s.SetSectionAble();
        }
    }
}
