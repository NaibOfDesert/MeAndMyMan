using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiController : MonoBehaviour
{
    [SerializeField] List<MenuUiTab> tabList;

    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;
    [SerializeField] private Sprite tabActiveSprite; 
    void Awake()
    {
        tabList = new List<MenuUiTab>();


    }

    void Update()
    {
        
    }

    public void AddToTabList(MenuUiTab menuUiTab)
    {
        tabList.Add(menuUiTab); 
    }

    public void OnTabEnter(MenuUiTab menuUiTab)
    {
        ResetTabs();
        menuUiTab.TabBackgroundImage.sprite = tabHoverSprite;

    }

    public void OnTabExit(MenuUiTab menuUiTab)
    {
        ResetTabs();

    }

    public void OnTabSelected(MenuUiTab menuUiTab)
    {
        menuUiTab.TabBackgroundImage.sprite = tabActiveSprite;

    }

    public void ResetTabs()
    {
        foreach(var menuUiTab in tabList)
        {
            menuUiTab.TabBackgroundImage.sprite = tabIdleSprite; 
        }
    }

}
