using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuUiSection : MonoBehaviour
{
        [SerializeField] private List<MenuUiSectionState> menuUiSectionStateList;

    [SerializeField] private MenuUiSectionState menuUiSectionState;
    public MenuUiSectionState MenuUiSectionState { get {return menuUiSectionState;} }
    [SerializeField] private List<string> menuUiStatesList; 
    public List<string> MenuUiStatesList { get {return menuUiStatesList;} }

    GameController gameController;
    MenuUiSectionController menuUiSectionController;
    

    private void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        menuUiSectionController = gameController.MenuUiSectionController;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        
        menuUiSectionController.AddToUiList(this);
        menuUiStatesList = new List<string>();

        GetStatesList(menuUiSectionState);
    }

    public void Start()
    {


    }

    public void SetSectionAble()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    private void GetStatesList <T>(T menuUiState)
    {
        var menuUiStateString = menuUiState.ToString(); 
        var menuUiStateList = String.Concat(menuUiStateString.Where(l => !char.IsWhiteSpace(l))).Split(",").ToList();
        
        foreach(var s in menuUiStateList)
        {
            menuUiStatesList.Add(s);
        }

        menuUiStatesList.RemoveAll(s => s == MenuUiSectionState.noneState.ToString() || s == MenuUiTabState.noneState.ToString()); // ?: to monit, can be unusefull
    }

}
