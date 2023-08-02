using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuUiSectionActive : MonoBehaviour
{
    [SerializeField] private EMenuUiState menuUiState;
    public EMenuUiState MenuUiState { get {return menuUiState;} }
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


    }

    public void SetSectionAble()
    {
        // Debug.Log(this.gameObject.activeSelf);
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
