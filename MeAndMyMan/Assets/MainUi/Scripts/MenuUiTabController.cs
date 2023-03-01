using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiTabController : MonoBehaviour
{
    [SerializeField] List<MenuUiTab> tabList;

    GameController gameController; 
    GameUiMenuController gameUiMenuController; 
    MenuUiTab activeTab;
    List<GameObject> activeTabObjects;  // to rename
    // [SerializeField] Transform[] tabTransformtList; // TODO: to add objects to  active

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        gameUiMenuController = gameController.GameUiMenuController; 
        
        tabList = new List<MenuUiTab>();
        activeTabObjects = new List<GameObject>();
        GetActiveTabObjectList();
    }

    void Update()
    {
        
    }

    public void GetActiveTabObjectList()
    {
        Transform[] tabTransformtList = GetComponentsInChildren<Transform>();

        foreach (var tabTransform in tabTransformtList)
        {
            activeTabObjects.Add(tabTransform.gameObject);
        }
    }
    public void AddToTabList(MenuUiTab menuUiTab)
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
        menuUiTab.SetTabActiveSprite(); 
        for(int i = 0; i < activeTabObjects.Count; i++)
        {
            if(i == menuUiTab.transform.GetSiblingIndex())
            {
                activeTabObjects[i].SetActive(true);
            }
            else
            {
                activeTabObjects[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(var menuUiTab in tabList)
        {
            if (activeTab != null && menuUiTab == activeTab) { continue; }
            menuUiTab.SetTabIdleSprite();
        }
    }





}
