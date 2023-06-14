using System.Collections.Generic;
using System;
using UnityEngine;

public class MenuUiTabController : MonoBehaviour
{
    [SerializeField] List<MenuUiTabButton> tabList;

    GameController gameController; 
    GameUiMenuController gameUiMenuController;
    MenuUiTabButton activeTab;

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
        tabList = new List<MenuUiTabButton>();

    }

    void Update()
    {
        
    }


    public void AddToUiList(MenuUiTabButton menuUiTab)
    {
        tabList.Add(menuUiTab); 
    }

    public void OnTabEnter(MenuUiTabButton menuUiTab)
    {
        ResetTabs();
        if (activeTab == null || menuUiTab != activeTab)
        {
            menuUiTab.SetTabHoverSprite();
        }
    }

    public void OnTabExit(MenuUiTabButton menuUiTab)
    {
        ResetTabs();
    }

    public void OnTabSelected(MenuUiTabButton menuUiTab)
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
        foreach(MenuUiTabButton menuUiTab in tabList)
        {
            if (activeTab != null && menuUiTab == activeTab) { continue; }
            menuUiTab.SetTabIdleSprite();
        }
    }





}
