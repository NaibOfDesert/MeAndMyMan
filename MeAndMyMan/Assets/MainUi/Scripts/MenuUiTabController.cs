using System.Collections.Generic;
using System;
using UnityEngine;

public class MenuUiTabController : MonoBehaviour
{
    [SerializeField] List<MenuUiTabActive> tabList;

    GameController gameController; 
    GameUiMenuController gameUiMenuController;
    MenuUiTabActive activeTab;

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
        tabList = new List<MenuUiTabActive>();

    }

    private void Start()
    {
    }

    void Update()
    {
        
    }

    public void AddToUiList(MenuUiTabActive menuUiTab)
    {
        tabList.Add(menuUiTab); 
    }

    public void OnTabEnter(MenuUiTabActive menuUiTab)
    {
        ResetTabs();
        if (activeTab == null || menuUiTab != activeTab)
        {
            menuUiTab.SetTabHoverSprite();
        }
    }

    public void OnTabExit(MenuUiTabActive menuUiTab)
    {
        ResetTabs();
    }

    public void OnTabSelected(MenuUiTabActive menuUiTab)
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
        foreach(MenuUiTabActive menuUiTab in tabList)
        {
            if (activeTab != null && menuUiTab == activeTab) { continue; }
            menuUiTab.SetTabIdleSprite();
        }
    }
}
