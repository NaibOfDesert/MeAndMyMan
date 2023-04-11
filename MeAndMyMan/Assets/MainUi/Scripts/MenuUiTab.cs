using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Image))]   
public class MenuUiTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite tabBlockSprite;
    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;
    [SerializeField] private Sprite tabActiveSprite;

    [Header("Values")]
    [SerializeField] private MenuUiState tabState;

    private Image tabBackgroundImage;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    bool isAble; 

    GameController gameController; 
    GameManager gameManager; 
    GameUiController gameUiController; 
    GameUiMenuController gameUiMenuController; 
    MenuUiTabController menuUiTabController;

    void Awake()
    {
        try
        {
        gameController = FindObjectOfType<GameController>();
        gameManager  = gameController.GameManager; 
        gameUiController = gameController.GameUiController; 
        gameUiMenuController = gameController.GameUiMenuController; 
        menuUiTabController = gameController.MenuUiTabController; 
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            // *: Debug.Log("MenuUiTab: Awake basic values launched correctly"); 
        }

        tabBackgroundImage = GetComponent<Image>();
    }


    void Update()
    {
        if(tabState == MenuUiState.infrastructureManageState)
        {
            // TODO: implement albe check
            // if(!gameManager.CheckBuildInfrastructure(
            //     gameUiMenuController.InfrastructureInControl.InfrastructureObject.ObjectType, 
            //     gameUiMenuController.InfrastructureInControl.InfrastructureObject.ObjectLevel))
            // {
            //     // is able true or false
            //     // throw new System.ArgumentOutOfRangeException(); // check
            //     return; 
            // }
        }
    }
    void Start()
    {
        isAble = true;
        menuUiTabController.AddToUiList(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isAble) menuUiTabController.OnTabSelected(this); 

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuUiTabController.OnTabEnter(this);
        // TODO: implement view of resorces to get
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuUiTabController.OnTabExit(this);
        // TODO: implement view of resorces to set as default

    }

    private void IsAbleCheck()
    {
        // if(isAble) ? isAble == true : isAble == false;
    }
    public void SetTabIdleSprite()
    {
        tabBackgroundImage.sprite = tabIdleSprite; 
    }
    public void SetTabHoverSprite()
    {
        tabBackgroundImage.sprite = tabHoverSprite;
    }
    public void SetTabActiveSprite()
    {
        tabBackgroundImage.sprite = tabActiveSprite;
    }

    public void Select()
    {
        if(onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
