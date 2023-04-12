using System.Collections.Generic;
using System;
using UnityEngine;

public class MenuUiTabController : MonoBehaviour
{
    [SerializeField] List<MenuUiTab> tabList;

    GameController gameController; 
    GameUiMenuController gameUiMenuController;
    MenuUiTab activeTab;

    void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        gameUiMenuController = gameController.GameUiMenuController; 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Start()
    {
        tabList = new List<MenuUiTab>();

    }

    void Update()
    {
        
    }


    public void AddToUiList(MenuUiTab menuUiTab)
    {
        tabList.Add(menuUiTab); 
    }

    public void OnTabEnter(MenuUiTab menuUiTab)
    {
        ResetTabs();
        if (activeTab == null || menuUiTab != activeTab)
        {
            menuUiTab.SetTabHoverSprite();
        }
    }

    public void OnTabExit(MenuUiTab menuUiTab)
    {
        ResetTabs();
    }

    public void OnTabSelected(MenuUiTab menuUiTab)
    {
        if(activeTab != null)
        {
            activeTab.Deselect();
        }
        activeTab = menuUiTab;
        activeTab.Select();

        ResetTabs();
        menuUiTab.SetTabIdleSprite();

    }

    public void ResetTabs()
    {
        foreach(MenuUiTab menuUiTab in tabList)
        {
            if (activeTab != null && menuUiTab == activeTab) { continue; }
            menuUiTab.SetTabIdleSprite();
        }
    }





}
