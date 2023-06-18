using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Linq;

[RequireComponent(typeof(Image))]   
public class MenuUiTabActive : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite tabBlockSprite;
    [SerializeField] private Sprite tabIdleSprite;
    [SerializeField] private Sprite tabHoverSprite;

    [Header("Values")]
    private Image tabBackgroundImage;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected; // ?: probably unusefull
    
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

        tabBackgroundImage = GetComponent<Image>();
        
        menuUiTabController.AddToUiList(this);
    }
    void Start()
    {

    }
    
    void Update()
    {

    }

    #region Pointers
    public void OnPointerClick(PointerEventData eventData)
    {
        menuUiTabController.OnTabSelected(this); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuUiTabController.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuUiTabController.OnTabExit(this);
    }
    #endregion


    #region SpriteControl
    public void SetTabIdleSprite()
    {
        tabBackgroundImage.sprite = tabIdleSprite; 
    }

    public void SetTabHoverSprite()
    {
        tabBackgroundImage.sprite = tabHoverSprite;
    }

    public void SetTabBlockSprite()
    {
        tabBackgroundImage.sprite = tabBlockSprite;
    }
    #endregion

    #region Selection
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
    #endregion
}
