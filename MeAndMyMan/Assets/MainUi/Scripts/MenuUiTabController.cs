using System.Collections.Generic;
using UnityEngine;

public class MenuUiTabController : MonoBehaviour, IMenuUiController
{
    [SerializeField] List<IMenuUi> tabList;

    GameController gameController; 
    GameUiMenuController gameUiMenuController;
    MenuUiTab activeTab;
    // [SerializeField] Transform[] tabTransformtList; // TODO: to add objects to  active

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameUiMenuController = gameController.GameUiMenuController; 
        
        tabList = new List<IMenuUi>();
    }

    void Update()
    {
        
    }


    public void AddToUiList(IMenuUi menuUiTab)
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
