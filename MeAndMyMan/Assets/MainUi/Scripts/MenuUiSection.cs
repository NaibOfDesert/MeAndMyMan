using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUiSection : MonoBehaviour
{
    [SerializeField] private MenuUiState menuUiState;
    public MenuUiState MenuUiState { get {return menuUiState;} }
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

    }

    public void Start()
    {
        // menuUiSectionController.AddToUiList(this);

    }

    public void SetSectionAble()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

}
